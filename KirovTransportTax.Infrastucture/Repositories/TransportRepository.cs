﻿using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly TransportDbConnection dbContext;
        private readonly Mapper mapperFrom = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<Transport, TransportDbModel>();
        }));
        private readonly Mapper mapperTo = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<TransportDbModel, Transport>();
        }));

        public TransportRepository(TransportDbConnection dbContext)
        {
            this.dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            dbContext.CommitTransaction();
        }

        public async Task<int> Create(Transport entity)
        {
            var model = mapperFrom.Map<TransportDbModel>(entity);
            return await dbContext.InsertAsync(model);
        }

        public async Task<int> Delete(Transport entity)
        {
            var model = mapperFrom.Map<TransportDbModel>(entity);
            return await dbContext.DeleteAsync(model);
        }

        public async Task<int> DeleteByNumber(string numberTransporPK)
        {
            return await dbContext.TransportDb
                .Where(t => t.NumberTransport.Equals(numberTransporPK))
                .DeleteAsync();
        }

        public async Task<int> DeleteByPassport(string passportFK)
        {
            return await dbContext.TransportDb
                .Where(t => t.DriverPassport.Equals(passportFK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<Transport>> GetAll()
        {
            var transports = await dbContext.TransportDb.ToListAsync();
            return transports.ConvertAll(t => mapperTo.Map<Transport>(t));
        }

        public async Task<Transport> GetByNumber(string numberTransporPK)
        {
            var transport = await dbContext.TransportDb
                .Where(t => t.NumberTransport.Equals(numberTransporPK))
                .FirstOrDefaultAsync();
            return mapperTo.Map<Transport>(transport);
        }

        public async Task<IEnumerable<Transport>> GetByPassport(string passportFK)
        {
            var transports = await dbContext.TransportDb
                .Where(t => t.DriverPassport.Equals(passportFK))
                .ToListAsync();
            return transports.ConvertAll(t => mapperTo.Map<Transport>(t));
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async Task<int> Update(Transport entity)
        {
            var model = mapperFrom.Map<TransportDbModel>(entity);
            return await dbContext.UpdateAsync(model);
        }

        public async Task<int> Update(string oldNumberTransporPK, Transport entity)
        {
            var model = mapperFrom.Map<TransportDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.TransportDb
                    .Where(t => t.NumberTransport.Equals(oldNumberTransporPK))
                    .Set(t => t.NumberTransport, model.NumberTransport)
                    .UpdateAsync();
                var updatedRows = await dbContext.UpdateAsync(model);
                CommitTransaction();
                return updatedRows;
            } catch
            {
                RollbackTransaction();
                return 0;
            }
        }
    }
}
