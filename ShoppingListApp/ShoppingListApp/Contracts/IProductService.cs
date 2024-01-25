using ShoppingListApp.Models;

namespace ShoppingListApp.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        
        Task<ProductViewModel> GetByIdAsync(int id);

        //Task<ProductViewModel> GetByNameAsync(string name);

        Task AddProductAsync(ProductViewModel model);

        Task UpdateProductAsync(ProductViewModel model);

        Task DeleteProductAsync(int id);
    }
}
