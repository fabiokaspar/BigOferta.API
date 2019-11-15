using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BigOferta.API.Data;
using BigOferta.API.Dtos;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOfferById(int id)
        {
            Offer offer = await _repo.GetOfferById(id);
            OfferForReturnDto offerToReturn = _mapper.Map<OfferForReturnDto>(offer);
            
            return Ok(offerToReturn);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllOffers(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            List<Offer> offers = await _repo.GetAllOffersForUser(userId);

            List<OfferForReturnDto> offersToReturn = _mapper.Map<List<Offer>, List<OfferForReturnDto>>(offers);
            
            return Ok(offersToReturn);
        }

        [HttpGet("hanked")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHankedOffers()
        {
            List<Offer> offers = await _repo.GetHankingOffers();
            List<OfferForReturnDto> offersToReturn = _mapper.Map<List<Offer>, List<OfferForReturnDto>>(offers);
            
            return Ok(offersToReturn);
        }

        [HttpGet("hanked/user/{userId}")]
        public async Task<IActionResult> GetHankedOffers(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            List<Offer> offers = await _repo.GetHankingOffersForUser(userId);
            List<OfferForReturnDto> offersToReturn = _mapper.Map<List<Offer>, List<OfferForReturnDto>>(offers);
            
            return Ok(offersToReturn);
        }
    }
}