using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
            => await _context.Seller.OrderBy(i => i.Name).ToListAsync();

        public async Task<Seller?> FindByIdAsync(int id)
            => await _context.Seller.Include(i => i.Department).FirstOrDefaultAsync(i => i.Id == id);

        public async Task InsertAsync(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var seller = _context.Seller.Find(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException("Can't delete seller because he/she has cadastred salles!");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            if (!await _context.Seller.AnyAsync(i => i.Id == seller.Id))
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
