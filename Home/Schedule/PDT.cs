using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01
{
    internal class PDT
    {
        MY_DB mydb = new MY_DB();
        public bool InsertData(string adv,string scheduleid,double rating,double price,string ngaytaikham)
        {

                    
                    // Tạo câu lệnh SQL chèn dữ liệu vào bảng PhieuDieuTri
                    string query = "INSERT INTO PhieuDieuTri (Advice,scheduleid,rating,price,ngaytaikham) VALUES (@Advise,@id,@rating,@price,@ngaytaikham)";

                    using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
                    {
                    mydb.openConnection();
                        // Thêm tham số cho câu lệnh SQL để tránh tình trạng SQL injection
                        command.Parameters.AddWithValue("@Advise", adv);
                    command.Parameters.AddWithValue("@id", scheduleid);
                command.Parameters.AddWithValue("@rating", rating);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@ngaytaikham", ngaytaikham);

                // Thực thi câu lệnh SQL
                command.ExecuteNonQuery();
                    mydb.closeConnection();
                    }

                    return true; // Trả về true nếu chèn thành công
          
   

        }
        public bool UpdateData(string adv, string scheduleid,double rating,double price)
        {
            // Tạo câu lệnh SQL cập nhật dữ liệu trong bảng PhieuDieuTri
            string query = "UPDATE PhieuDieuTri SET Advice = @Advise, rating = @rating,price = @price WHERE scheduleid = @id";

            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                mydb.openConnection();
                // Thêm tham số cho câu lệnh SQL để tránh tình trạng SQL injection
                command.Parameters.AddWithValue("@Advise", adv);
                command.Parameters.AddWithValue("@id", scheduleid);
                command.Parameters.AddWithValue("@rating", rating);
                command.Parameters.AddWithValue("@price", price);
                // Thực thi câu lệnh SQL
                int rowsAffected = command.ExecuteNonQuery();
                mydb.closeConnection();

                // Trả về true nếu có ít nhất một bản ghi được cập nhật
                return rowsAffected > 0;
            }
        }

        public DataTable getPDT(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;
        }
    }
}
