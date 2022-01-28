using Domain.Entities;
using Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repository
{
    public class Repository : IRepository
    {
        private readonly Rh_estrategiaContext _rh_EstrategiaContext;
        public Repository(Rh_estrategiaContext rh_EstrategiaContext)
        {
            this._rh_EstrategiaContext = rh_EstrategiaContext;
        }
        public  Pessoa[] GetAllPessoasAsync()
        {
            IQueryable<Pessoa> query = _rh_EstrategiaContext.Pessoas;

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id);

            return query.ToArray();
        }
       
        public Pessoa GetPessoaById(int pessoaId)
        {
            IQueryable<Pessoa> query = _rh_EstrategiaContext.Pessoas;

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(pessoa => pessoa.Id == pessoaId);

            return query.FirstOrDefault();
        }

        public void Add<T>(T entity) where T : class
        {
            _rh_EstrategiaContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _rh_EstrategiaContext.Remove(entity);
        }
        public bool SaveChanges()
        {
            return (_rh_EstrategiaContext.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _rh_EstrategiaContext.Update(entity);
        }

        
    }
}
