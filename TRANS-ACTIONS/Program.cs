using System.Data.SqlClient;

namespace TRANS_ACTIONS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            TRANSTEst  tran = new TRANSTEst();
            tran.Error();
        }
    }
    class TRANSTEst
    {
        string ConStr = @"Server=206-4\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";

        public void TransactionTest1()
        {
            using (SqlConnection db = new SqlConnection(ConStr))
            {
                db.Open();
                SqlTransaction tran = db.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into account_1 values(1,100)", db))
                    {
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                    using (SqlCommand cmd = new SqlCommand("insert into account_1 values(1,100)", db))
                    {
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                    Console.WriteLine("all ok");
                    tran.Commit();
                }
                catch (Exception)
                {
                    Console.WriteLine("err error!");
                    tran.Rollback();
                }

                //реализовать три таблицы с полями баланса на счетах положить 200 на
                //первый счет сложить количество денег с первого и второго счета
                //и сложить полученное со вторым столбцом 
                //добавить чек в таблицу три не дозволять транзакций больше 500 
                db.Close();
            }
        }
        public void TransactionTask1()
        {
            using (SqlConnection db = new SqlConnection(ConStr))
            {
                db.Open();
                SqlTransaction tran = db.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into table1 values(1,100)", db))
                    {
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                    using (SqlCommand cmd = new SqlCommand("insert into table2 values(1,700 + (select balance from table1 where id = 1))", db))
                    {
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                    using (SqlCommand cmd = new SqlCommand("insert into table3 values(1,100 + (select balance from table2 where id = 1))", db))
                    {
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                    Console.WriteLine("all ok");
                    tran.Commit();
                }
                catch (Exception)
                {
                    Console.WriteLine("err error!");
                    tran.Rollback();
                }


                db.Close();
            }
        }
        public void Error()
        {
            using (SqlConnection db = new SqlConnection(ConStr))
            {
                db.Open();
                try
                {
                    Program program;
                    program.ToString();
                    Int32.Parse("aaaaaa");
                    using (SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[test] VALUES {1}  {10}"))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("ok");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                db.Close();
            }
        }
    }

}