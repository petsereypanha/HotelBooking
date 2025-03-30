using Microsoft.Data.SqlClient;
                        
                        namespace HotelBookingSystem
                        {
                            public sealed class DatabaseManager
                            {
                                private static readonly Lazy<DatabaseManager> _instance = new Lazy<DatabaseManager>(() => new DatabaseManager());
                                private readonly string _connectionString;
                                private SqlConnection _connection;

                                private DatabaseManager()
                                {
                                    // Use SQL Server Authentication instead of Windows Authentication
                                    // to connect to Docker SQL Server
                                    _connectionString = "Server=127.0.0.1,1433;Initial Catalog=master;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";
                                    _connection = new SqlConnection(_connectionString);
                                }

                                public static DatabaseManager Instance => _instance.Value;

                                public SqlConnection GetConnection()
                                {
                                    try
                                    {
                                        if (_connection == null || _connection.State == System.Data.ConnectionState.Closed)
                                        {
                                            _connection = new SqlConnection(_connectionString); // Create new connection
                                            _connection.Open();
                                        }
                                        return _connection;
                                    }
                                    catch (SqlException ex)
                                    {
                                        throw new Exception("Error connecting to the database: " + ex.Message, ex);
                                    }
                                }

                                public void CloseConnection()
                                {
                                    try
                                    {
                                        if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                                        {
                                            _connection.Close();
                                            _connection.Dispose();
                                            _connection = null;
                                        }
                                    }
                                    catch (SqlException ex)
                                    {
                                        throw new Exception("Error closing the database connection: " + ex.Message, ex);
                                    }
                                }
                            }
                        }