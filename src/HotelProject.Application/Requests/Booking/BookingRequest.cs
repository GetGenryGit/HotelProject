namespace HotelProject.Application.Requests.Booking;

public class BookingRequest
{
    public Guid UserId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;

    private int personCount = 1;
    public int PersonCount
    {
        get => personCount;
        set => personCount = value < 1 || value >= 5 ? 1 : value;
    }

    private DateTime checkinDate = DateTime.Now.Date.AddHours(10);
    public DateTime CheckinDate
    {
        get => checkinDate;
        set
        {
            checkinDate = value == DateTime.MinValue ? DateTime.Now.Date.AddHours(10) : value;
            if (CheckoutDate.Date <= CheckinDate.Date)
                CheckoutDate = CheckoutDate.AddDays(1);
        }
    }

    private DateTime checkoutDate = DateTime.Now.Date.AddDays(1).AddHours(12);
    public DateTime CheckoutDate
    {
        get => checkoutDate;
        set
        {
            checkoutDate = value == DateTime.MinValue ? DateTime.Now.Date.AddDays(1).AddHours(12) : value;
            if (CheckinDate.Date >= CheckoutDate.Date)
                CheckinDate = CheckoutDate.AddDays(-1).AddHours(-2);
        }
    }

    public bool IsNeedCleaningRoom { get; set; }

}
