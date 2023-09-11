using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly SimpleTraderDbContext _context;

        public AccountDataService(SimpleTraderDbContext context)
        {
            _context = context;
        }

        public async Task<Account> Create(Account entity)
        {
            var createdResult = await _context.Accounts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            Account entity = await _context.Accounts.FirstOrDefaultAsync(e => e.Id == id);
            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Account> Get(int id)
        {
            Account entity = await _context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            return entity;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            IEnumerable<Account> entities = await _context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .ToListAsync();
            
            return entities;
        }

        public async Task<Account> GetByEmail(string email)
        {
            return await _context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
        }

        public async Task<Account> GetByUserName(string userName)
        {
            return await _context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.UserName == userName);
        }

        public async Task<Account> Update(int id, Account entity)
        {
            entity.Id = id;
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
