using Microsoft.Data.SqlClient; // Use this instead of System.Data.SqlClient for modern .NET
using System;
using System.Data;

namespace CalculatorMVC.Models
{
    public class CalculationRepository
    {
        // Use your connection string here. Make sure it is correct.
        private string connectionString = "Data Source=(localdb)\\local;Initial Catalog=CalculatorDB;User ID=sa;Password=password";

        public void SaveCalculation(CalculationModel calculation)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Operations (Number1, Number2, Operation, Result) VALUES (@Number1, @Number2, @Operation, @Result)";
                Console.Write("query");
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Using explicit parameter types
                    cmd.Parameters.Add("@Number1", SqlDbType.Decimal).Value = calculation.Number1;
                    cmd.Parameters.Add("@Number2", SqlDbType.Decimal).Value = calculation.Number2;
                    cmd.Parameters.Add("@Operation", SqlDbType.VarChar).Value = calculation.Operation;
                    cmd.Parameters.Add("@Result", SqlDbType.Decimal).Value = calculation.Result;

                    try
                    {
                        con.Open();
                        int

rowsAffected = cmd.ExecuteNonQuery();

                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
