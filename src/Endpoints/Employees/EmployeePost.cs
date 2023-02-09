using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;
//endpoint tendo so a logica de cadastrar employee
public class EmployeePost {

    // => ao criar a propriedade template ele ja esta setando o valor "/employee"
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    //injetando o servico UserManager do tipo <IdentityUser>
    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager) {
        //instanciando a classe IdentityUser
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;
        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim> {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };
        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);

    }

}
