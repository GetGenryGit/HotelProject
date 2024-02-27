using HotelProject.Application.Interfaces.Repositories.Bookings;
using HotelProject.Application.Interfaces.Repositories.Rooms;
using HotelProject.Application.Interfaces.Services.Rooms;
using HotelProject.Application.Responses.Room;
using HotelProject.Domain.Entities;
using HotelProject.Infrastructure.Repositories.Rooms;
using HotelProject.Shared.Wrapper;
using System.Xml.Linq;

namespace HotelProject.Infrastructure.Services.Rooms;

public class RoomsService(IRoomsRepository roomsRepository,
    IRoomTypeRepository roomTypeRepository,
    IBookingRepository bookingRepository)
    : IRoomsService
{
    private readonly IRoomsRepository _roomsRepository = roomsRepository;
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    #region [Rooms]

    public async Task<Result<RoomResponse>> GetById(Guid id,
        DateTime checkinDate, DateTime checkoutDate)
    {
        var roomEntity = await _roomsRepository.GetById(id);
        if (roomEntity == null)
        {
            return await Result<RoomResponse>
                .FailAsync($"Комната с данным айди не существует!");
        }

        var bookings = await _bookingRepository.GetByDates(checkinDate, checkoutDate);

        var roomResponse = new RoomResponse()
        {
            Id = roomEntity.Id,

            TypeName = roomEntity.Type.Name,
            TypeDescription = roomEntity.Type.Description,
            PriceOneNight = roomEntity.Type.PriceOneNight,

            Name = roomEntity.Name,
            FloorLevel = roomEntity.FloorLevel,

            IsActive = roomEntity.IsActive,
            IsCleaned = roomEntity.IsCleaned,
            IsRoomNotBusy = !bookings.Any(b => b.RoomId == roomEntity.Id)
        };

        return await Result<RoomResponse>
            .SuccessAsync(roomResponse);
    }
    public async Task<Result<List<RoomResponse>>> GetRoomsByDates(DateTime checkinDate, DateTime checkoutDate)
    {
        var roomsEntities = await _roomsRepository.Get();

        var bookings = await _bookingRepository.GetByDates(checkinDate, checkoutDate);

        var roomsResponse = new List<RoomResponse>();
        
        foreach (var roomsEntity in roomsEntities)
        {
            roomsResponse.Add(
                new() 
                {
                    Id = roomsEntity.Id,

                    TypeName = roomsEntity.Type.Name,
                    TypeDescription = roomsEntity.Type.Description,
                    PriceOneNight = roomsEntity.Type.PriceOneNight,

                    Name = roomsEntity.Name,
                    FloorLevel = roomsEntity.FloorLevel,

                    IsActive = roomsEntity.IsActive,
                    IsCleaned = roomsEntity.IsCleaned,
                    IsRoomNotBusy = !bookings.Any(b => b.RoomId == roomsEntity.Id)
                });
        }

        return await Result<List<RoomResponse>>
            .SuccessAsync(roomsResponse);
    }

    public async Task<Result> AddRoom(Guid typeId, string name, int floorLevel)
    {
        var roomWithSameName = await _roomsRepository.GetByName(name);
        if (roomWithSameName != null)
        {
            return await Result<string>
                .FailAsync($"Комната с именем {name} уже существует!");
        }

        await _roomsRepository.Add(typeId, name, floorLevel);
        return await Result<string>
            .SuccessAsync($"Комната {name} успешно добавлена!");
    }


    public async Task<Result> UpdateRoom(Guid id, Guid typeId,
        string name, int floorLevel,
        bool isCleaned, bool isActive)
    {
        await _roomsRepository.Update(id, typeId, 
            name, floorLevel, isCleaned, isActive);

        return await Result<string>
            .SuccessAsync($"Комната {name} успешно обновлена!");
    }

    public async Task<Result> DeleteRoom(Guid id)
    {
        await _roomsRepository.Delete(id);
        return await Result<string>
            .SuccessAsync($"Комната успешно удалена!");
    }


    #endregion

    #region [RoomTypes]
    public async Task<Result<List<RoomTypeResponse>>> GetRoomTypes()
    {
        var types = await _roomTypeRepository.Get();

        var typesResponse = new List<RoomTypeResponse>();

        foreach (var t in types)
        {
            typesResponse.Add(
                new()
                {
                    Id = t.Id,

                    Name = t.Name,
                    Description = t.Description,
                    PriceOneNight = t.PriceOneNight,
 
                    MaxPersons = t.MaxPersons
                });
        }

        return await Result<List<RoomTypeResponse>> 
            .SuccessAsync(typesResponse);
    }

    public async Task<Result> AddRoomType(string name, 
        string description, decimal priceOneNight, int maxPersons)
    {
        var typeSameName = await _roomTypeRepository.GetByName(name);
        if (typeSameName != null)
        {
            return await Result<string>
                .FailAsync($"Тип с именем {name} уже существует!");
        }

        await _roomTypeRepository.Add(name, description, priceOneNight, maxPersons);
        return await Result<string>
            .SuccessAsync("Новый тип комнат добавлен успешно!");
    }

    public async Task<Result> UpdateRoomType(Guid id,
        string name, string description, decimal priceOneNight, int maxPersons)
    {
        await _roomTypeRepository.Update(id, name, description, priceOneNight, maxPersons);
        return await Result<string>
            .SuccessAsync($"Тип комнат {name} успешно обновлен!");
    }

    public async Task<Result> DeleteRoomType(Guid id)
    {
        await _roomTypeRepository.Delete(id);
        return await Result<string>
            .SuccessAsync($"Тип комнат успешно удален!");
    }

    #endregion


}
