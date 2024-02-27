using HotelProject.Application.Interfaces.Repositories.Bookings;
using HotelProject.Application.Interfaces.Repositories.Rooms;
using HotelProject.Application.Interfaces.Services.Booking;
using HotelProject.Application.Responses.Booking;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Infrastructure.Services.Bookings;

public class BookingsService(IBookingRepository bookingRepository,
    IBookingStatusRepository bookingStatusRepository,
    IRoomsRepository roomsRepository)
    : IBookingsService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IBookingStatusRepository _bookingStatusRepository = bookingStatusRepository;
    private readonly IRoomsRepository _roomsRepository = roomsRepository;

    #region [Bookings]

    public async Task<Result<List<BookingResponse>>> GetAllBookings()
    {
        var bookings = await _bookingRepository.Get();

        var bookingsResponse = new List<BookingResponse>();

        foreach (var b in bookings)
        {
            bookingsResponse.Add(
                new()
                {
                    Id = b.Id,

                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName, 
                    MiddleName = b.User.MiddleName,

                    RoomName = b.Room.Name,

                    CheckinDate = b.CheckinDate,
                    CheckoutDate = b.CheckoutDate,

                    PersonCount = b.PersonCount,
                    TotalPrice = b.TotalPrice,
                    StatusName = b.Status.Name,

                    Created = b.Created
                });
        }

        return await Result<List<BookingResponse>>
            .SuccessAsync(bookingsResponse);
    }

    public async Task<Result<List<BookingResponse>>> GetAllBookingByUserId(Guid id)
    {
        var bookings = await _bookingRepository.GetByUserId(id);

        var bookingsResponse = new List<BookingResponse>();

        foreach (var b in bookings)
        {
            bookingsResponse.Add(
                new()
                {
                    Id = b.Id,

                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName,
                    MiddleName = b.User.MiddleName,

                    RoomName = b.Room.Name,

                    CheckinDate = b.CheckinDate,
                    CheckoutDate = b.CheckoutDate,

                    PersonCount = b.PersonCount,
                    TotalPrice = b.TotalPrice,

                    StatusName = b.Status.Name,

                    Created = b.Created
                });
        }

        return await Result<List<BookingResponse>>
            .SuccessAsync(bookingsResponse);
    }

    public async Task<Result<BookingResponse>> GetBookingById(Guid id)
    {
        var bookingEntity = await _bookingRepository.GetBookingById(id);
        if (bookingEntity == null)
        {
            return await Result<BookingResponse>
               .FailAsync($"Брони с данным идентификатором не существует!");
        }

        var bookingResponse = new BookingResponse()
        {
            Id = bookingEntity.Id,

            FirstName = bookingEntity.User.FirstName,
            LastName = bookingEntity.User.LastName,
            MiddleName = bookingEntity.User.MiddleName,

            RoomName = bookingEntity.Room.Name,

            CheckinDate = bookingEntity.CheckinDate,
            CheckoutDate = bookingEntity.CheckoutDate,

            PersonCount = bookingEntity.PersonCount,
            TotalPrice = bookingEntity.TotalPrice,
            StatusName = bookingEntity.Status.Name,


            Created = bookingEntity.Created
        };

        return await Result<BookingResponse>
            .SuccessAsync(bookingResponse);
    }


    public async Task<Result> UpdateBooking(Guid id, string roomName, string statusName,
        DateTime checkinDate, DateTime checkoutDate, int personCount, bool isNeedCleaningRoom)
    {
        var statusEntity = await _bookingStatusRepository.GetByName(statusName);
        if (statusEntity == null)
        {
            return await Result<string>
                .FailAsync($"Статуса с именем {statusName} не существует!");
        }

        var roomEntity = await _roomsRepository.GetByName(roomName);
        if (roomEntity == null) 
        {
            return await Result<string>
                .FailAsync($"Комнаты с айди {roomName} не существует!");
        }

        var days = checkoutDate.Day - checkinDate.Day;
        var totalPrice = roomEntity.Type
            .PriceOneNight * days;

        if (isNeedCleaningRoom)
        {
            await _roomsRepository.Update(roomEntity.Id, roomEntity.TypeId, 
                roomEntity.Name, roomEntity.FloorLevel, false, roomEntity.IsActive);
        }

        await _bookingRepository.Update(id, roomEntity.Id, statusEntity.Id,
            checkinDate, checkoutDate, totalPrice, personCount);

        return await Result<string>
            .SuccessAsync("Заявка для бронирования успешно обновлена!");
    }
    public async Task<Result> AddBooking(Guid userId, string roomName, string statusName,
        DateTime checkinDate, DateTime checkoutDate, int personCount)
    {
        var statusEntity = await _bookingStatusRepository.GetByName(statusName);
        if (statusEntity == null)
        {
            return await Result<string>
                .FailAsync($"Статуса с именем {statusName} не существует!");
        }

        var roomEntity = await _roomsRepository.GetByName(roomName);
        if (roomEntity == null)
        {
            return await Result<string>
                .FailAsync($"Комнаты с айди {roomName} не существует!");
        }

        if (!roomEntity.IsActive)
        {
            return await Result<string>
               .FailAsync($"Данная комната в данный момент не активна!");
        }

        var days = checkoutDate.Day - checkinDate.Day;
        var totalPrice = roomEntity.Type
            .PriceOneNight * days;

        if (personCount > roomEntity.Type.MaxPersons)
        {
            return await Result<string>
                .FailAsync($"Превышено максимальное количество человек в комнате!");
        }

       /* var bookingsWithSameDates = await _bookingRepository.GetByDatesForRoom(roomEntity.Id, checkinDate, checkoutDate);
        if (bookingsWithSameDates.Count != 0)
        {
            return await Result<string>
                .FailAsync($"Данная комната занята в выбранных числах!");
        }*/

        await _bookingRepository.Add(roomEntity.Id, userId, statusEntity.Id,
            checkinDate, checkoutDate, totalPrice, personCount);

        return await Result<string>
            .SuccessAsync("Заявка для бронирования успешно обработана!");
    }
    #endregion

    #region [BookingStatuses]
    public async Task<Result<List<BookingStatusResponse>>> GetBookingStatuses()
    {
        var bookingStatuses = await _bookingStatusRepository.Get();

        var bookingStatusesResponse = new List<BookingStatusResponse>();

        foreach (var bS in bookingStatuses)
        {
            bookingStatusesResponse.Add(
                new()
                {
                    Id = bS.Id, 

                    Name = bS.Name,
                    Description = bS.Description,
                });
        }

        return await Result<List<BookingStatusResponse>>
            .SuccessAsync(bookingStatusesResponse);
    }

    public async Task<Result> AddBookingStatus(string name, string description)
    {
        var statusSameName = await _bookingStatusRepository.GetByName(name);
        if (statusSameName != null)
        {
            return await Result<string>
                .FailAsync("Статус с таким же именем уже существует!");
        }

        await _bookingStatusRepository.Add(name, description);
        return await Result<string>
            .SuccessAsync("Новый статус успешно добавлен");
    }

    public async Task<Result> UpdateBookingStatus(Guid id, string name, string description)
    {
        await _bookingStatusRepository.Update(id, name, description);
        return await Result<string>
            .SuccessAsync($"В статус {name} успешно внесены измения");
    }

    public async Task<Result> DeleteBookingStatus(Guid id)
    {
        await _bookingStatusRepository.Delete(id);
        return await Result<string>
            .SuccessAsync($"Статус успешно удален!");
    }


    #endregion
}
