namespace api
{
    public class ConnectionString
    {
        public string cs { get; set; }

        public ConnectionString()
        {
            string server = "td5l74lo6615qq42.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";

            string database = "l2znj4xjg2pekeiv"; 

            string port = "3306";

            string username = "p98itka9dqh0a195";

            string password = "rr06gixrxr7v2nui";

            cs = $@"server = {server}; username = {username}; database = {database}; port = {port}; password ={password};";
        }
    }
}
