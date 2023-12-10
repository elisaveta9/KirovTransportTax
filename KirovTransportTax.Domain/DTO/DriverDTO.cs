namespace KirovTransportTax.Domain.DTO
{
    public class DriverDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
        public List<TransportDTO> Transports { get; set; }

        public DriverDTO(int id, string lastName, string name, string patronymic, DateOnly birthday, List<TransportDTO> transports)
        {
            Id = id;
            LastName = lastName;
            Name = name;
            Patronymic = patronymic;
            Birthday = birthday;
            Transports = transports;
        }
    }
}
