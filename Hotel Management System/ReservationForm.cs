using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class ReservationForm : Form
    {
        RoomClass room = new RoomClass();
        ReservationClass reservation = new ReservationClass();
        public ReservationForm()
        {
            InitializeComponent();
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            //To Show a room type in combobox
            comboBox_roomType.DataSource = room.getRoomType();
            comboBox_roomType.DisplayMember = "Label";
            comboBox_roomType.ValueMember = "CategoryId";

            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            comboBox_roomNo.DataSource = reservation.roomByType(type);
            comboBox_roomNo.DisplayMember = "RoomId";
            comboBox_roomNo.ValueMember = "RoomId";

            dataGridView_reserv.DefaultCellStyle.ForeColor = Color.Black;
            //To show Reservation list in DatagridView
            getReservTable();

            
        }
        public void getReservTable()
        {
            dataGridView_reserv.DataSource = reservation.getReserv();
        }

        private void comboBox_roomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // display room's number depending on the room type
            // display the room's status is free
            try
            {
                int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
                comboBox_roomNo.DataSource = reservation.roomByType(type);
                comboBox_roomNo.DisplayMember = "RoomId";
                comboBox_roomNo.ValueMember = "RoomId";
            }
            catch (Exception)
            { 
                //Nothing
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                string guestId = textBox_guestId.Text;
                string roomNo = this.comboBox_roomNo.GetItemText(this.comboBox_roomNo.SelectedItem);

                DateTime dIn = dateTimePicker_dateIn.Value;
                DateTime dOut = dateTimePicker_dateOut.Value;

                //date in must be equal or > Today date and
                // date out must be equal or > date IN ok?
                if (dIn < DateTime.Today)
                {
                    MessageBox.Show("Reservation Date In must be Today Or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dOut < dIn)
                {
                    MessageBox.Show("Reservation Date Out must be same Date In and After", "invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (reservation.addReserv(guestId, roomNo, dIn, dOut) && reservation.setReservRoom(roomNo, "Busy"))
                    {
                        getReservTable();
                        MessageBox.Show("New Reservation added successfuly", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Reservation has not been added into the database", "Error Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int reserId = Convert.ToInt32(textBox_reservId.Text);
            string rno = comboBox_roomNo.Text;

            try
            {
                if (reservation.removeReserv(reserId)&& reservation.setReservRoom(rno,"Free"))
                {
                    getReservTable();
                    MessageBox.Show("Reservation has been deleted from the database", "Deleted Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Delete",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView_reserv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_reservId.Text = dataGridView_reserv.CurrentRow.Cells[0].Value.ToString();
            textBox_guestId.Text = dataGridView_reserv.CurrentRow.Cells[1].Value.ToString();

            string rno = dataGridView_reserv.CurrentRow.Cells[2].Value.ToString();
            comboBox_roomType.SelectedValue = reservation.typeByRoomNo(rno);
            comboBox_roomNo.Text = rno;
            //comboBox_roomType
            dateTimePicker_dateIn.Text = dataGridView_reserv.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker_dateOut.Text = dataGridView_reserv.CurrentRow.Cells[4].Value.ToString();
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_reservId.Clear();
            textBox_guestId.Clear();
            comboBox_roomType.SelectedValue = 1;
            dateTimePicker_dateIn.Value = DateTime.Now;
            dateTimePicker_dateOut.Value = DateTime.Now;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(textBox_reservId.Text);
                string guestId = textBox_guestId.Text;
                //string roomNo = comboBox_roomNo.SelectedValue.ToString();
                string roomNo = this.comboBox_roomNo.GetItemText(this.comboBox_roomNo.SelectedItem);

                DateTime dIn = dateTimePicker_dateIn.Value;
                DateTime dOut = dateTimePicker_dateOut.Value;

                //date in must be equal or > Today date and
                // date out must be equal or > date IN ok?
                if (dIn < DateTime.Today)
                {
                    MessageBox.Show("Reservation Date In must be Today Or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dOut < dIn)
                {
                    MessageBox.Show("Reservation Date Out must be same Date In and After", "invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (reservation.editReserv(reservId, guestId, roomNo, dIn, dOut) && reservation.setReservRoom(roomNo, "Busy"))
                    {
                        getReservTable();
                        MessageBox.Show("Reservation has been updated into the database", "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Reservation has not been updated into the database", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.White;
        }*/

    }
}
