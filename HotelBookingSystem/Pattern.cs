using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelBookingSystem
{
    internal class Pattern
    {
    }
    // Singleton - Database Connection
    public class Database
    {
        private static Database instance;
        [Obsolete]
        private SqlConnection connection;
        private Database()
        {
            connection = new SqlConnection("your_connection_string_here");
        }
        public static Database Instance
        {
            get
            {
                if (instance == null)
                    instance = new Database();
                return instance;
            }
        }
        public SqlConnection GetConnection() => connection;
    }

    // Factory Method - Room Types
    public abstract class Room
    {
        public abstract string GetRoomType();
    }
    public class StandardRoom : Room
    {
        public override string GetRoomType() => "Standard Room";
    }
    public class DeluxeRoom : Room
    {
        public override string GetRoomType() => "Deluxe Room";
    }
    public class RoomFactory
    {
        public static Room CreateRoom(string type)
        {
            return type switch
            {
                "Standard" => new StandardRoom(),
                "Deluxe" => new DeluxeRoom(),
                _ => throw new ArgumentException("Invalid Room Type")
            };
        }
    }

    // Observer - Booking Notifications
    public interface IObserver
    {
        void Update(string message);
    }
    public class Admin : IObserver
    {
        public void Update(string message)
        {
            MessageBox.Show("Admin Notification: " + message);
        }
    }
    public class Customer : IObserver
    {
        public void Update(string message)
        {
            MessageBox.Show("Customer Notification: " + message);
        }
    }
    public class BookingSubject
    {
        private List<IObserver> observers = new List<IObserver>();
        public void Attach(IObserver observer) => observers.Add(observer);
        public void Notify(string message)
        {
            foreach (var observer in observers)
                observer.Update(message);
        }
    }

    // Decorator - Room Add-ons
    public abstract class RoomDecorator : Room
    {
        protected Room room;
        public RoomDecorator(Room room) => this.room = room;
        public override string GetRoomType() => room.GetRoomType();
    }
    public class WiFiDecorator : RoomDecorator
    {
        public WiFiDecorator(Room room) : base(room) { }
        public override string GetRoomType() => base.GetRoomType() + " with WiFi";
    }
    public class BreakfastDecorator : RoomDecorator
    {
        public BreakfastDecorator(Room room) : base(room) { }
        public override string GetRoomType() => base.GetRoomType() + " with Breakfast";
    }

    // Strategy - Payment Methods
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(double amount) => MessageBox.Show("Paid " + amount + " using Credit Card");
    }
    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(double amount) => MessageBox.Show("Paid " + amount + " using PayPal");
    }
    public class CashPayment : IPaymentStrategy
    {
        public void Pay(double amount) => MessageBox.Show("Paid " + amount + " using Cash");
    }

}
