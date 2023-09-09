using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public interface IMatrialRepository<T> : IRepository<T> where T : Matrial
    {
        bool isCodeExist(string Code);
        bool isIDExist(int id);
    }
}
