using Northstar.WS.Models;
using Northstar.WS.Models.DTO;
using Northstar.WS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

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

        public string GetUserRoleInfo(int roleId)
        {
            return _context.UserRoles.FirstOrDefault(ur => ur.UserRoleId == roleId).RoleName.ToString();
        }

        public List<UserDTO> GetUsers()
        {
            var users = _context.Users.ToList();
            List<UserDTO> usersToReturn = new List<UserDTO>();
            foreach (var user in users)
            {
                usersToReturn.Add(
                    new UserDTO
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Role = new UserRoleDTO()
                        {
                            UserRoleName = GetUserRoleInfo(user.UserRoleId)
                        }
                    });
            }
            return usersToReturn;
        }

        public bool RegisterUser(UserDTO user)
        {
            var userToBeAdded = new User
            {
                Email = user.Email,
                UserName = user.UserName,
                UserRoleId = (int)user.Role.UserRoleId,
                Password = PasswordHelper.ComputeHash(user.Password, "SHA512", null),
                Created = DateTime.Now,
                LastModified = DateTime.Now
            };
            try
            {
                _context.Users.Add(userToBeAdded);
                _context.SaveChanges();
                SetSuccessResponse(201, CurrentController, user);
                return true;
            }
            catch (DbUpdateException)
            {
                SetErrorResponse(302, resourceName: CurrentController, obj: user);
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
