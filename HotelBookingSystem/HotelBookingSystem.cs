using System;
                    using System.Collections.Generic;
                    using System.Data;
                    using Microsoft.Data.SqlClient;
                    using System.Linq;
                    
                    namespace HotelBookingSystem
                    {
                        public class Hotel
                        {
                            public int HotelId { get; set; }
                            public string HotelName { get; set; }
                            public string Address { get; set; }
                            public int Rating { get; set; }
                            public string ImagePath { get; set; }
                        }
                    
                        public class RoomType
                        {
                            public int RoomTypeId { get; set; }
                            public string TypeName { get; set; }
                            public decimal BasePrice { get; set; }
                        }
                    
                        public class Room
                        {
                            public int RoomId { get; set; }
                            public int HotelId { get; set; }
                            public int RoomTypeId { get; set; }
                            public string RoomNumber { get; set; }
                            public int BedCount { get; set; }
                            public bool IsAvailable { get; set; }
                    
                            public Hotel Hotel { get; set; }
                            public RoomType RoomType { get; set; }
                        }
                    
                        public class HotelManager
                        {
                            private readonly DatabaseManager _dbManager;
                    
                            public HotelManager()
                            {
                                _dbManager = DatabaseManager.Instance;
                            }
                    
                            public int AddHotel(string name, string address, int rating, string imagePath)
                            {
                                string query = "INSERT INTO Hotels (HotelName, Address, Rating, ImagePath) VALUES (@Name, @Address, @Rating, @ImagePath); SELECT SCOPE_IDENTITY();";
                                SqlParameter[] parameters = {
                                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                                    new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address },
                                    new SqlParameter("@Rating", SqlDbType.Int) { Value = rating },
                                    new SqlParameter("@ImagePath", SqlDbType.NVarChar) { Value = imagePath }
                                };
                    
                                using (var connection = _dbManager.GetConnection())
                                {
                                    using (var command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddRange(parameters);
                                        return Convert.ToInt32(command.ExecuteScalar());
                                    }
                                }
                            }
                    
                            public List<Hotel> GetAllHotels()
                            {
                                string query = "SELECT * FROM Hotels";
                                using (var connection = _dbManager.GetConnection())
                                {
                                    using (var command = new SqlCommand(query, connection))
                                    {
                                        using (var reader = command.ExecuteReader())
                                        {
                                            var hotels = new List<Hotel>();
                                            while (reader.Read())
                                            {
                                                hotels.Add(new Hotel
                                                {
                                                    HotelId = reader.GetInt32(0),
                                                    HotelName = reader.GetString(1),
                                                    Address = reader.GetString(2),
                                                    Rating = reader.GetInt32(3),
                                                    ImagePath = reader.GetString(4)
                                                });
                                            }
                                            return hotels;
                                        }
                                    }
                                }
                            }
                    
                            public int AddBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut, decimal totalPrice, bool hasBreakfast, bool hasGymSpa, bool hasBarAccess)
                            {
                                string query = @"INSERT INTO Bookings (GuestId, RoomId, CheckInDate, CheckOutDate, TotalPrice, HasBreakfast, HasGymSpa, HasBarAccess) 
                                                 VALUES (@GuestId, @RoomId, @CheckIn, @CheckOut, @TotalPrice, @HasBreakfast, @HasGymSpa, @HasBarAccess); 
                                                 SELECT SCOPE_IDENTITY();";
                                SqlParameter[] parameters = {
                                    new SqlParameter("@GuestId", SqlDbType.Int) { Value = guestId },
                                    new SqlParameter("@RoomId", SqlDbType.Int) { Value = roomId },
                                    new SqlParameter("@CheckIn", SqlDbType.DateTime) { Value = checkIn },
                                    new SqlParameter("@CheckOut", SqlDbType.DateTime) { Value = checkOut },
                                    new SqlParameter("@TotalPrice", SqlDbType.Decimal) { Value = totalPrice },
                                    new SqlParameter("@HasBreakfast", SqlDbType.Bit) { Value = hasBreakfast },
                                    new SqlParameter("@HasGymSpa", SqlDbType.Bit) { Value = hasGymSpa },
                                    new SqlParameter("@HasBarAccess", SqlDbType.Bit) { Value = hasBarAccess }
                                };
                    
                                using (var connection = _dbManager.GetConnection())
                                {
                                    using (var command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddRange(parameters);
                                        return Convert.ToInt32(command.ExecuteScalar());
                                    }
                                }
                            }
                    
                            public int AddGuest(string name, string email = null, string phone = null)
                            {
                                string query = "INSERT INTO Guests (GuestName, Email, Phone) VALUES (@Name, @Email, @Phone); SELECT SCOPE_IDENTITY();";
                                SqlParameter[] parameters = {
                                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                                    new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email ?? (object)DBNull.Value },
                                    new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = phone ?? (object)DBNull.Value }
                                };
                    
                                using (var connection = _dbManager.GetConnection())
                                {
                                    using (var command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddRange(parameters);
                                        return Convert.ToInt32(command.ExecuteScalar());
                                    }
                                }
                            }
                    
                            public List<Room> GetAvailableRooms(string roomType, int bedCount, DateTime checkIn, DateTime checkOut)
                            {
                                if (checkIn >= checkOut)
                                    throw new ArgumentException("Check-in date must be before check-out date");
                    
                                string query = @"SELECT r.RoomId, r.RoomNumber, h.HotelName, rt.TypeName, r.BedCount, rt.BasePrice
                                                 FROM Rooms r
                                                 JOIN Hotels h ON r.HotelId = h.HotelId
                                                 JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                                 WHERE r.IsAvailable = 1
                                                 AND (@RoomType IS NULL OR rt.TypeName = @RoomType)
                                                 AND (@BedCount = 0 OR r.BedCount = @BedCount)
                                                 AND r.RoomId NOT IN (
                                                     SELECT RoomId FROM Bookings
                                                     WHERE Status <> 'Cancelled'
                                                     AND (CheckInDate < @CheckOut AND CheckOutDate > @CheckIn)
                                                 )
                                                 ORDER BY rt.BasePrice ASC";
                                SqlParameter[] parameters = {
                                    new SqlParameter("@RoomType", SqlDbType.NVarChar, 50) { Value = string.IsNullOrEmpty(roomType) ? (object)DBNull.Value : roomType },
                                    new SqlParameter("@BedCount", SqlDbType.Int) { Value = bedCount },
                                    new SqlParameter("@CheckIn", SqlDbType.DateTime) { Value = checkIn },
                                    new SqlParameter("@CheckOut", SqlDbType.DateTime) { Value = checkOut }
                                };
                    
                                using (var connection = _dbManager.GetConnection())
                                {
                                    using (var command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddRange(parameters);
                                        using (var reader = command.ExecuteReader())
                                        {
                                            var rooms = new List<Room>();
                                            while (reader.Read())
                                            {
                                                rooms.Add(new Room
                                                {
                                                    RoomId = reader.GetInt32(0),
                                                    RoomNumber = reader.GetString(1),
                                                    Hotel = new Hotel { HotelName = reader.GetString(2) },
                                                    RoomType = new RoomType { TypeName = reader.GetString(3) },
                                                    BedCount = reader.GetInt32(4),
                                                    IsAvailable = true
                                                });
                                            }
                                            return rooms;
                                        }
                                    }
                                }
                            }
                        }
                    }