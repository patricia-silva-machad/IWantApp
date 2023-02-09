using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;
//endpoint tendo so a logica de cadastrar employee
public class EmployeesPost {

    // => ao criar a propriedade template ele ja esta setando o valor "/employee"
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    //injetando o servico UserManager do tipo <IdentityUser>
    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager) {
        //instanciando a classe IdentityUser
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;
        if(!result.Succeeded) 
            return Results.BadRequest(result.Errors.First());


        return Results.Created($"/employees/{user.Id}", user.Id);

    }

}
