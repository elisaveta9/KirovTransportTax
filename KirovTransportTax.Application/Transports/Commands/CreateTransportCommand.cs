using AutoMapper;
using KirovTransportTax.Application.Brands.Commands;
using KirovTransportTax.Application.Drivers.Commands;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Application.TransportModels.Commands;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Transports.Commands
{
    public class CreateTransportCommand
    {
        private readonly ITransportRepository transportRepository;
        private readonly Lazy<Mapper> mapperBrand = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Brand>();
        })));

        private readonly Lazy<Mapper> mapperTransportModel = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, TransportModel>();
        })));

        private readonly Lazy<Mapper> mapperDriver = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Driver>();
        })));

        private readonly Lazy<Mapper> mapperTransport = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Transport>();
        })));

        public CreateTransportCommand(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public bool Execute(Transport transport)
        {
            return transportRepository.Create(transport).Result == 1;
        }

        public bool Execute(CreateTransportDto transport,
            CreateBrandCommand createBrandCommand,
            CreateTransportModelCommand createTransportModelCommand,
            CreateDriverCommand createDriverCommand)
        {
            transportRepository.BeginTransaction();
            try
            {
                createBrandCommand.Execute(mapperBrand.Value.Map<Brand>(transport));
                createTransportModelCommand.Execute(mapperTransportModel.Value.Map<TransportModel>(transport));
                createDriverCommand.Execute(mapperDriver.Value.Map<Driver>(transport));
                var result = Execute(mapperTransport.Value.Map<Transport>(transport));
                transportRepository.CommitTransaction();
                return result;
            } catch
            {
                transportRepository.RollbackTransaction();
                return false;
            }
        }
    }
}
