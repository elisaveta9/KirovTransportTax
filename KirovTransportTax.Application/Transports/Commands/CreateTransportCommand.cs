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
        private readonly CreateBrandCommand createBrandCommand;
        private readonly CreateTransportModelCommand createTransportModelCommand;
        private readonly CreateDriverCommand createDriverCommand;

        private readonly Lazy<Mapper> mapperBrand = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Brand>()
            .ForMember(nameof(Brand.Name), opt => opt.MapFrom(src => src.Brand));
        })));

        private readonly Lazy<Mapper> mapperTransportModel = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, TransportModel>();
        })));

        private readonly Lazy<Mapper> mapperDriver = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Driver>()
            .ForMember(nameof(Driver.Passport), opt => opt.MapFrom(src => src.DriverPassport))
            .ForMember(nameof(Driver.LastName), opt => opt.MapFrom(src => src.DriverLastName))
            .ForMember(nameof(Driver.Name), opt => opt.MapFrom(src => src.DriverName))
            .ForMember(nameof(Driver.Patronymic), opt => opt.MapFrom(src => src.DriverPatronymic))
            .ForMember(nameof(Driver.Birthday), opt => opt.MapFrom(src => src.DriverBirthday));
        })));

        private readonly Lazy<Mapper> mapperTransport = new(new Mapper(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<CreateTransportDto, Transport>();
        })));

        public CreateTransportCommand(ITransportRepository transportRepository, 
            CreateBrandCommand createBrandCommand, 
            CreateTransportModelCommand createTransportModelCommand, 
            CreateDriverCommand createDriverCommand)
        {
            this.transportRepository = transportRepository;
            this.createBrandCommand = createBrandCommand;
            this.createTransportModelCommand = createTransportModelCommand;
            this.createDriverCommand = createDriverCommand;
        }

        public bool Execute(Transport transport)
        {
            return transportRepository.Create(transport).Result != 0;
        }

        public bool Execute(CreateTransportDto transport)
        {
            try { 
                createBrandCommand.Execute(mapperBrand.Value.Map<Brand>(transport));
                createTransportModelCommand.Execute(mapperTransportModel.Value.Map<TransportModel>(transport));
                createDriverCommand.Execute(mapperDriver.Value.Map<Driver>(transport));
                var result = Execute(mapperTransport.Value.Map<Transport>(transport));
                return result;
            } catch
            {
                return false;
            }
        }
    }
}
