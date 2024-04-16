using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр7
{
    public partial class Form1 : Form
    {
        // Структура для хранения информации об авиарейсе
        public struct Flight
        {
            public string FlightNumber;
            public string Destination;
            public string DepartureTime;
            public DateTime DepartureDate;
            public double TicketPrice;
            public int SeatsAvailable;
        }

        // Список для хранения всех авиарейсов
        List<Flight> flights = new List<Flight>();

        public Form1()
        {
            InitializeComponent();
        }

        private void AddFlight(string flightNumber, string destination, string departureTime, DateTime departureDate, double ticketPrice, int seatsAvailable)
        {
            if (string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(destination) || string.IsNullOrWhiteSpace(departureTime) || ticketPrice <= 0 || seatsAvailable <= 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректными данными.");
                return;
            }

            flights.Add(new Flight
            {
                FlightNumber = flightNumber,
                Destination = destination,
                DepartureTime = departureTime,
                DepartureDate = departureDate,
                TicketPrice = ticketPrice,
                SeatsAvailable = seatsAvailable
            });

            DisplayFlights();
        }

        // Функция для отображения всех авиарейсов в DataGridView
        private void DisplayFlights()
        {
            dataGridView1.Rows.Clear();
            foreach (var flight in flights)
            {
                dataGridView1.Rows.Add(flight.FlightNumber, flight.Destination, flight.DepartureTime, flight.DepartureDate.ToShortDateString(), flight.TicketPrice, flight.SeatsAvailable);
            }
        }

        // Функция для отображения результатов поиска в DataGridView
        private void DisplaySearchResults(List<Flight> searchResults)
        {
            dataGridView1.Rows.Clear();
            foreach (var flight in searchResults)
            {
                dataGridView1.Rows.Add(flight.FlightNumber, flight.Destination, flight.DepartureTime, flight.DepartureDate.ToShortDateString(), flight.TicketPrice, flight.SeatsAvailable);
            }
        }

        // Функция для поиска авиарейсов по пункту назначения
        private List<Flight> SearchByDestination(string destination)
        {
            List<Flight> searchResults = new List<Flight>();
            foreach (var flight in flights)
            {
                if (flight.Destination.Contains(destination))
                {
                    searchResults.Add(flight);
                }
            }
            return searchResults;
        }

        // Функция для удаления всех авиарейсов
        private void RemoveAllFlights()
        {
            flights.Clear();
            DisplayFlights();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string flightNumber = textBox1.Text;
            string destination = textBox2.Text;
            string departureTime = textBox3.Text;
            DateTime departureDate = dateTimePicker1.Value;
            double ticketPrice;
            int seatsAvailable;

            if (!double.TryParse(textBox4.Text, out ticketPrice) || !int.TryParse(textBox5.Text, out seatsAvailable))
            {
                MessageBox.Show("Пожалуйста, введите корректную цену билета и количество доступных мест.");
                return;
            }

            AddFlight(flightNumber, destination, departureTime, departureDate, ticketPrice, seatsAvailable);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchDestination = textBox6.Text;
            List<Flight> searchResults = SearchByDestination(searchDestination);
            DisplaySearchResults(searchResults);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Text = DateTime.Now.ToString("HH:mm");
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RemoveAllFlights();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Установка формата времени для текстового поля "Время вылета"
            textBox3.Text = DateTime.Now.ToString("HH:mm");
            // Настройка DataGridView
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Номер рейса";
            dataGridView1.Columns[1].Name = "Пункт назначения";
            dataGridView1.Columns[2].Name = "Время вылета";
            dataGridView1.Columns[3].Name = "Дата вылета";
            dataGridView1.Columns[4].Name = "Стоимость билета";
            dataGridView1.Columns[5].Name = "Кол-во мест";
        }
    }
}
