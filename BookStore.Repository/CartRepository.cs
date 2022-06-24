﻿using BookStore.Models.DataModels;
using BookStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public List<Cart> GetCartItems(string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return query.ToList();

        }
        public Cart GetCarts(int id)
        {
            return _context.Carts.SingleOrDefault(c => c.Id == id);
        }
        public Cart AddCart(Cart cart)
        {

            var entry = _context.Carts.Add(cart);
            _context.SaveChanges();
            return entry.Entity;
        }
        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();

            return entry.Entity;
        }
        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return false;
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return true;
        }
    }
}
