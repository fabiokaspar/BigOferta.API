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
// using System.Linq;

namespace BigOferta.API.Controllers
{
    [Route("bowebapi/[controller]")]
    [ApiController]
    [Authorize]
    public class OfertasController : ControllerBase
    {
        private readonly DatingRepository _repo;
        private readonly IMapper _mapper;
        public OfertasController(DatingRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllOffers()
        {
            List<Offer> offers = await _repo.GetAllOffers();
            List<OfferForReturnDto> offersToReturn = _mapper.Map<List<Offer>, List<OfferForReturnDto>>(offers);

            return Ok(offersToReturn);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredOffers([FromQuery]OfferParams offerParams)
        {
            List<Offer> offers = await _repo.GetOffersByFiltering(offerParams);
            var offersToReturn = _mapper.Map<List<OfferForReturnDto>>(offers);

            return Ok(offersToReturn);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllOffersForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            List<Offer> offers = await _repo.GetAllOffersForUser(userId);

            List<OfferForReturnDto> offersToReturn = _mapper.Map<List<Offer>, List<OfferForReturnDto>>(offers);

            return Ok(offersToReturn);
        }

        [HttpGet("filter/user/{userId}")]
        public async Task<IActionResult> GetFilteredOffersForUser(int userId, 
            [FromQuery]OfferParams offerParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            List<Offer> offers = await _repo.GetOffersByFiltering(offerParams);

            List<Offer> offersCart = await _repo.GetOffersCart(userId);
            
            foreach(var offer in offersCart)
            {
                offers.RemoveAll(off => off.Id == offer.Id);
            }

            var offersToReturn = _mapper.Map<List<OfferForReturnDto>>(offers);

            return Ok(offersToReturn);
        }

        [HttpGet("like")]
        [AllowAnonymous]
        public IActionResult GetOffersByMatching([FromQuery]OfferParams offerParams)
        {
            List<Offer> offers = _repo.GetOffersByMatching(offerParams);
            List<OfferForReturnDto> offersForReturnDto = _mapper.Map<List<OfferForReturnDto>>(offers);
        
            return Ok(offersForReturnDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> SaveOffer([FromQuery]int userId,
            OfferForRegisterDto offerForRegisterDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Offer newOffer = _mapper.Map<Offer>(offerForRegisterDto);
            
            _repo.Add(newOffer);

            if (await _repo.SaveAllAsync())
            {
                OfferForReturnDto offerForReturnDto = _mapper.Map<OfferForReturnDto>(newOffer);

                return Ok(offerForReturnDto);
            }

            return BadRequest("It's not possible saving offer");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOffer([FromQuery]int userId,
            OfferForRegisterDto offerForRegisterDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (await _repo.OfferExists(offerForRegisterDto.Id))
            {
                Offer offer = _mapper.Map<Offer>(offerForRegisterDto);
                _repo.Update(offer);

                if (await _repo.SaveAllAsync())
                {
                    OfferForReturnDto offerForReturnDto = _mapper.Map<OfferForReturnDto>(offer);

                    return Ok(offerForReturnDto);
                }

                return BadRequest("It was not possible updating offer");
            }

            return BadRequest("Offer doesn't exists.");
        }

        [HttpDelete("{offerId}")]
        public async Task<IActionResult> RemoveOfferById([FromQuery]int userId, int offerId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var offer = await _repo.GetOfferById(offerId);

            if (offer == null)
                return BadRequest("Offer doesn't exist");

            _repo.Delete(offer);
            
            if (await _repo.SaveAllAsync())
            {
                return Ok(offer);
            }

            return BadRequest("Fail in removing offer");
        }


        [HttpGet("{id}", Name = "GetOfferById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOfferById(int id)
        {
            Offer offer = await _repo.GetOfferById(id);
            OfferForReturnDto offerToReturn = _mapper.Map<OfferForReturnDto>(offer);

            return Ok(offerToReturn);
        }
    }
}