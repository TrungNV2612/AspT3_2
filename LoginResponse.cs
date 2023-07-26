using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspT3
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public Employee? Employee { get; set; }
        public LoginResponse() { }
        public Employee? FindByUserPass (string username, string password)
        {
            using var context = new EmployeeDbContext();
            var emp = (from e in context.Employee
                       where e.Name == username
                       where e.Password == password
                       select e).FirstOrDefault();
            this.Employee = emp;
            return emp;
        }
        public string CreateToken(Employee emp)
        {
            // 1. Tao key để thực hiện ký trên jwt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key trungnv"));

            // 2. List claims --> chính là phần payload
            List<Claim> claims = new List<Claim>
                {
                    new Claim("Id", emp.Id.ToString()),
                    new Claim("Name", emp.Name),
                    new Claim("Email", emp.Email),
                    new Claim("host", "https://npcetc.com.vn"),
                };

            // 3. Tạo chữ ký
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // 4. Tạo Token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
               );

            // 5. Lấy token dưới dạng string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            this.Token = jwt;
            return jwt;
        }

        //verify
        public bool VerifyJwt(string jwt)
        {
            try
            {
                // 1. taoj key để xác thực
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key trungnv"));
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Get("AppConfig.Secret"));

                SecurityToken stoken;

                // 2. sử dụng phương thức validateToken
                var jwtHandler = new JwtSecurityTokenHandler().ValidateToken(jwt, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out stoken);

                var jwtToken = (JwtSecurityToken)stoken;
                var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                Console.WriteLine(userName);
                Console.WriteLine(jwtHandler.Claims.First());
                Console.WriteLine(stoken);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
