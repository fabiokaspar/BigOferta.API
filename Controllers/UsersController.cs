using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BigOferta.API.Data;
using BigOferta.API.Dtos;
using BigOferta.API.Helpers;
using BigOferta.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigOferta.API.Controllers
{
    [Route("bowebapi/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(DatingRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            User user = await _repo.GetUser(userId);
            UserForReturnDto userForReturnDto = _mapper.Map<UserForReturnDto>(user);

            userForReturnDto.Purchase = _mapper.Map<PurchaseOrderDto>(user.CartOffers);
            // userForReturnDto.Purchase = _mapper.Map<PurchaseOrderDto>(user);
            userForReturnDto.Purchase.ClientId = user.Id;

            return Ok(userForReturnDto);
        }

        [HttpGet("{userId}/cart")]
        public async Task<IActionResult> GetOffersCart(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            List<UserOffer> userOffers = await _repo.GetUserOffersCart(userId);
            List<OfferForCartDto> offerForCartDtos;

            offerForCartDtos = Utils.GetOfferCartDtoList(userOffers, _mapper);

            return Ok(offerForCartDtos);
        }

        [HttpPost("{userId}/addToCart")]
        public async Task<IActionResult> AddOfferToCartForUser(int userId, OfferForCartDto offerForCartDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            List<Offer> offersFromUser = await _repo.GetOffersCart(userId);

            if (!offersFromUser.Exists(off => off.Id == offerForCartDto.OfferId))
            {
                UserOffer userOffer = new UserOffer 
                { 
                    Amount = offerForCartDto.Amount,
                    UserId = userId,
                    OfferId = offerForCartDto.OfferId
                };

                _repo.Add(userOffer);
            }
            else
            {
                List<UserOffer> list = await _repo.GetUserOffersCart(userId);
                var userOffer = list.Find(uo => uo.OfferId == offerForCartDto.OfferId);
                userOffer.Amount = offerForCartDto.Amount;
                _repo.Update(userOffer);
            }
            
            if (await _repo.SaveAllAsync())
            {
                List<UserOffer> userOffers = await _repo.GetUserOffersCart(userId);
                List<OfferForCartDto> offerForCartDtos;

                offerForCartDtos = Utils.GetOfferCartDtoList(userOffers, _mapper);
                return Ok(offerForCartDtos);
            }

            return BadRequest("It wasn't possible adding offer to the cart.");
        }

        [HttpPost("{userId}/removeFromCart")]
        public async Task<IActionResult> RemoveOfferFromCartForUser(int userId, OfferForCartDto offerForCartDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            List<Offer> offersFromUser = await _repo.GetOffersCart(userId);

            if (!offersFromUser.Exists(off => off.Id == offerForCartDto.OfferId))
            {
                return BadRequest("Offer does not exist in user's cart.");
            }

            List<UserOffer> list = await _repo.GetUserOffersCart(userId);
            var userOffer = list.Find(uo => uo.OfferId == offerForCartDto.OfferId);

            if (offerForCartDto.Amount == 0)
            {
                _repo.Delete(userOffer);
            }
            else
            {
                userOffer.Amount = offerForCartDto.Amount;
                _repo.Update(userOffer);
            }
            
            if (await _repo.SaveAllAsync())
            {
                List<UserOffer> userOffers = await _repo.GetUserOffersCart(userId);
                List<OfferForCartDto> offerForCartDtos;

                offerForCartDtos = Utils.GetOfferCartDtoList(userOffers, _mapper);
                return Ok(offerForCartDtos);
            }

            return BadRequest("It wasn't possible removing offer from cart.");
        }

        [HttpGet("{userId}/confirmPurchase")]
        public async Task<IActionResult> ConfirmPurchaseOrderForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            User user = await _repo.GetUser(userId);
            List<Offer> cart = await _repo.GetOffersCart(userId);

            if (cart.Count == 0)
                return BadRequest("Não há itens no carrinho do usuário");
            
            if (await _repo.SavePurchaseOrderForUser(user))
            {
                // UserForReturnDto userForReturnDto = _mapper.Map<UserForReturnDto>(user);
                PurchaseOrderDto purchaseDto = _mapper
                    .Map<PurchaseOrderDto>(user.CartOffers);

                cart = await _repo.ClearCartForUser(user);
                List<OfferForCartDto> listCart = null;

                if (cart != null)
                {
                    listCart = _mapper.Map<List<OfferForCartDto>>(cart);
                }

                return Ok(new {
                    purchase = purchaseDto,
                    cart = listCart
                });
            }

            return BadRequest("Houve algum problema em salvar a compra no banco");
        }

        [HttpGet("{userId}/lastPurchase")]
        public async Task<IActionResult> GetLastPurchaseForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            PurchaseOrder<UserOffer> purchase = await _repo.GetLastPurchaseForUser(userId);
            var purchaseDto = _mapper.Map<PurchaseOrderDto>(purchase);

            return Ok(purchaseDto);
        }

        [HttpGet("{userId}/userOffers")]
        public async Task<IActionResult> GetUserOffers(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            List<UserOffer> list = await _repo.GetUserOffersCart(userId);
            
            return Ok(list);
        }
    }
}