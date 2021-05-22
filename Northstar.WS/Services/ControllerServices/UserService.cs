using Northstar.WS.Models;
using Northstar.WS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services.ControllerServices
{
    public class UserService : BaseService, IUserService
    {
        private readonly AvimoreDBContext _context;

        public UserService(AvimoreDBContext context)
        {
            _context = context;
            CurrentController = CommonConstants.ResourceNameForUserController;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool RegisterUser(User user)
        {
            var userToBeAdded = user;
            userToBeAdded.Password = PasswordHelper.ComputeHash(user.Password, "SHA512", null);
            userToBeAdded.Created = DateTime.Now;
            userToBeAdded.LastModified = DateTime.Now;
            try
            {
                _context.Users.Add(userToBeAdded);
                _context.SaveChanges();
                SetSuccessResponse(201, CurrentController, userToBeAdded);
                return true;
            }
            catch (DbUpdateException)
            {
                SetErrorResponse(302, resourceName: CurrentController, obj: userToBeAdded);
                return false;
            }
            catch (Exception)
            {
                SetErrorResponse(102, resourceName: CurrentController);
                return false;
            }
        }
    }
}
