using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01
{
    internal class PDT
    {
        MY_DB mydb = new MY_DB();
        public bool InsertData(string adv, string lotrinh,string scheduleid)
        {

                    
                    // Tạo câu lệnh SQL chèn dữ liệu vào bảng PhieuDieuTri
                    string query = "INSERT INTO PhieuDieuTri (Advice, LoTrinh,scheduleid) VALUES (@Advise, @LoTrinh,@id)";

                    using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
                    {
                    mydb.openConnection();
                        // Thêm tham số cho câu lệnh SQL để tránh tình trạng SQL injection
                        command.Parameters.AddWithValue("@Advise", adv);
                        command.Parameters.AddWithValue("@LoTrinh", lotrinh);
                    command.Parameters.AddWithValue("@id", scheduleid);

                    // Thực thi câu lệnh SQL
                    command.ExecuteNonQuery();
                    mydb.closeConnection();
                    }

                    return true; // Trả về true nếu chèn thành công
          
   

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
