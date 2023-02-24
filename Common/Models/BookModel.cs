using BookstoreWebApp.Common.Interfaces;

namespace BookstoreWebApp.Common.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        bool _isReserved;
        public bool IsReserved
        {
            get => _isReserved;
            set
            {
                if (!value) _bookingNumber = null;
                else _bookingNumber = GenerateBookingNumber();

                _isReserved = value;
            }
        }

        string? _bookingNumber = null;
        public string? BookingNumber
        {
            get => _bookingNumber;
            set => _bookingNumber = value;
        }

        string GenerateBookingNumber()
        {
            string trimmedId = Id.ToString().Substring(0, 3);

            return $"{trimmedId}_{DateTime.UtcNow.ToString("yyyy-MM-dd")}";
        }
    }
}
