using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigOferta.API.Dtos;
using BigOferta.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BigOferta.API.Data
{
    public class DatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T: class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T: class
        {
            _context.Update(entity);
        }

        public bool SaveAll()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        
        public async Task<List<Offer>> GetAllOffers()
        {
            List<Offer> offers = await _context.Offers
                                    .Include(of => of.Photos)
                                    .ToListAsync();
            return offers;
        }

        public async Task<List<Offer>> GetHankingOffers()
        {
            return await _context.Offers
                            .Where(offer => offer.IsHanked)
                            .Include(of => of.Photos)
                            .ToListAsync();
        }

        public async Task<List<Offer>> GetAllOffersForUser(int userId)
        {
            List<Offer> allOffers = await GetAllOffers();
                        
            List<Offer> offersCart = await GetOffersCart(userId);
            
            foreach(var offer in offersCart)
            {
                allOffers.RemoveAll(off => off.Id == offer.Id);
            }

            return allOffers;
        }
        

        public async Task<List<Offer>> GetHankingOffersForUser(int userId)
        {
            List<Offer> hankedOffers = await GetHankingOffers();
            
            List<Offer> offersCart = await GetOffersCart(userId);

            foreach(var offer in offersCart)
            {
                hankedOffers.RemoveAll(off => off.Id == offer.Id);
            }

            return hankedOffers;
        }

        public async Task<Offer> GetOfferById(int id)
        {
            return await _context.Offers
                    .Include(off => off.Photos)
                    .FirstOrDefaultAsync(off => off.Id == id);
        }

        public async Task<User> GetUser(int userId)
        {
            User user = await _context.Users
                        .Include(u => u.CartOffers)
                        .Include(u => u.ProfilePhoto)
                        .FirstOrDefaultAsync(u => u.Id == userId);
            
            return user;
        }

        public async Task<List<UserOffer>> GetUserOffersCart(int userId)
        {
            var userOffers = await _context.UserOffers.Where(uo => uo.UserId == userId)
                                .Include(uo => uo.Offer)
                                .Include(uo => uo.Offer.Photos)
                                .ToListAsync();
            
            return userOffers;
        }

        public async Task<PurchaseOrder<UserOffer>> GetLastPurchaseForUser(int userId)
        {
            var purchase = await _context.PurchaseOrders.Where(p => p.ClientId == userId)
                                .OrderByDescending(p => p.DateOfPurchase)
                                .FirstOrDefaultAsync();
                
            return purchase;
        }

        public async Task<List<Offer>> GetOffersCart(int userId)
        {
            List<UserOffer> userOffers = await GetUserOffersCart(userId);
            List<Offer> offersCart = new List<Offer>();
            
            userOffers.ForEach(uo => {
                offersCart.Add(uo.Offer);    
            });

            return offersCart;
        }

        public async Task<bool> SavePurchaseOrderForUser(User user)
        {
            List<UserOffer> list = await GetUserOffersCart(user.Id);

            user.CartOffers = new PurchaseOrder<UserOffer>();
            user.CartOffers.ClientId = user.Id;
            
            foreach(var item in list)
            {
                user.CartOffers.Add(item);
            }

            user.CartOffers.confirmPurchaseOrder();
            
            Add(user.CartOffers);

            bool success = await SaveAllAsync();

            return success;
        }

        public async Task<List<Offer>> ClearCartForUser(User user)
        {
            // user.CartOffers = await GetPurchaseOrderForUser(user);
            
            List<UserOffer> list = await _context.UserOffers
                                        // .Include(uo => uo.User)
                                        // .Include(uo => uo.Offer)
                                        .Where(uo => uo.UserId == user.Id).ToListAsync();
            
            list.ForEach(uo => {
                Delete(uo);
            });

            if (await SaveAllAsync())
            {
                return await GetOffersCart(user.Id);
            }

            return null;
        }

    }
}