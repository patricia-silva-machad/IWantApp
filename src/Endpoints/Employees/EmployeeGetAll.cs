using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;
//endpoint tendo so a logica de cadastrar employee
public class EmployeeGetAll {

    // => ao criar a propriedade template ele ja esta setando o valor "/employee"
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    //injetando o servico UserManager do tipo <IdentityUser>
    public static IResult Action(UserManager<IdentityUser> userManager) {
      
        var users = userManager.Users.ToList();
        var employees = new List<EmployeeResponse>();
        foreach(var item in users) {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }
        return Results.Ok(employees);

    }

}
