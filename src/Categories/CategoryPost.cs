using IWantApp.Domain.Products;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Categories;
//endpoint tendo so a logica de cadastrar a categoria
public class CategoryPost {
    
    // => ao criar a propriedade template ele ja esta setando o valor "/categories"
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context) 
        {

        var category = new Category {
            
                Name = categoryRequest.Name,
                CreatedBy = "Test",
                CreatedOn = DateTime.Now,
                EditedBy = "Test",
                EditedOn = DateTime.Now,
            };
            context.Categories.Add(category);
            context.SaveChanges();

            return Results.Created($"/categories/{category.Id}", category.Id);

    }

}
