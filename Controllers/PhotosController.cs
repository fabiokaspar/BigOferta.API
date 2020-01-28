using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BigOferta.API.Data;
using BigOferta.API.Dtos;
using BigOferta.API.Helpers;
using BigOferta.API.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BigOferta.API.Controllers
{
    [Authorize]
    [Route("bowebapi/users/{userId}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly DatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(DatingRepository repo, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._cloudinaryConfig = cloudinaryConfig;
            this._mapper = mapper;
            this._repo = repo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("addPhotoToProfile")]
        public async Task<IActionResult> AddPhotoToProfile(int userId, 
            [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            User user = await _repo.GetUser(userId);

            if (user.ProfilePhoto != null)
            {
                Photo trashPhoto = await _repo.GetPhotoById(user.ProfilePhoto.Id);
                _repo.Delete(trashPhoto);
            }

            photoForCreationDto.Width = 500;
            photoForCreationDto.Height = 500;
            photoForCreationDto.UserId = userId;
            photoForCreationDto.IsMain = true;

            Photo photo = UploadPhotoToCloudinary(photoForCreationDto);

            _repo.Add(photo);

            if (await _repo.SaveAllAsync())
            {
                PhotoForReturnDto photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

                return Ok(photoForReturnDto);
            }

            return BadRequest("Adding photo to user's profile could not be possible");
        }


        [HttpPost("addPhotoToOffer/{offerId}")]
        public async Task<IActionResult> AddPhotoToOffer(int userId, int offerId,
            [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Photo photo = UploadPhotoToCloudinary(photoForCreationDto);
            var offer = await _repo.GetOfferById(offerId);

            photo.IsMain = false;
            
            offer.Photos.Add(photo);
            _repo.Update(offer);
            
            if (await _repo.SaveAllAsync())
            {
                var photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

                return CreatedAtRoute("GetPhotoById", new {  photoId = photo.Id }, photoForReturnDto);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("addPhotoToNewOffer")]
        public async Task<IActionResult> AddPhotoToNewOffer(int userId,
            [FromForm]PhotoForCreationDto photoForCreationDto,
            [FromForm]OfferForRegisterDto offerForRegisterDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Photo photo = UploadPhotoToCloudinary(photoForCreationDto);
            photo.IsMain = true;

            Offer offer = await _repo.GetOfferById(offerForRegisterDto.Id);

            if (offer == null)
            {
                offer = _mapper.Map<Offer>(offerForRegisterDto);           
                offer.Photos.Add(photo);
                _repo.Add(offer);    
            }
            else
            {
                offer.Photos.Add(photo);
                _repo.Update(offer);   
            }

            if (await _repo.SaveAllAsync())
            {
                var offerForReturnDto = _mapper.Map<OfferForReturnDto>(offer);

                return CreatedAtRoute("GetOfferById", new { controller = "ofertas", id = offerForReturnDto.Id }, offerForReturnDto);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{photoId}", Name="GetPhotoById")]
        public async Task<IActionResult> GetPhotoById(int photoId)
        {
            Photo photo = await _repo.GetPhotoById(photoId);
            PhotoForReturnDto photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

            return Ok(photoForReturnDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int userId, int photoId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            Photo photo = await _repo.GetPhotoById(photoId);

            if (photo == null)
            {
                return BadRequest("Photo doesn't exist");
            }

            _repo.Delete(photo);
            
            if (await _repo.SaveAllAsync())
            {
                PhotoForReturnDto photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

                return Ok(photoForReturnDto);
            }

            return BadRequest("Fail in removing photo");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserProfilePhoto(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            User user = await _repo.GetUser(userId);

            Photo photo = user.ProfilePhoto;
            
            if (photo == null)
            {
                return BadRequest("Photo doesn't exist");
            }

            _repo.Delete(photo);
            
            if (await _repo.SaveAllAsync())
            {
                PhotoForReturnDto photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

                return Ok(photoForReturnDto);
            }

            return BadRequest("Fail in removing photo");
        }

        private Photo UploadPhotoToCloudinary(PhotoForCreationDto photoForCreationDto)
        {
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(photoForCreationDto.Width)
                            .Height(photoForCreationDto.Height)
                            .Crop("fill")
                            .Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            Photo photo = _mapper.Map<Photo>(photoForCreationDto);

            return photo;
        }

    }
}