using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    internal interface ICategoryRepository : IReposirtory<Category>
    {
        void update(Category category);
        void save();
    }
}
