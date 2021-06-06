﻿using RecipeIngredientShoppingSystem.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RecipeIngredientShoppingSystem.MVC.Models
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }



        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }



        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }



        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }



        public override string[] GetRolesForUser(string username)
        {
            using (Sprint1Entities db = new Sprint1Entities())
            {
                
                var UserRoles = (from user in db.UserLogins
                                 
                                 
                                 where user.UserName == username
                                 select user.Role).ToArray();
                return UserRoles;
            }
        }



        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }



        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}