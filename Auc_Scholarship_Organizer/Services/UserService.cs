using Auc_Scholarship_Organizer.DTOs;
using Auc_Scholarship_Organizer.Models;
using Auc_Scholarship_Organizer.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Services
{
    public class UserService
    {
        UserRepository _userRepo;
        public UserService()
        {
            _userRepo = new UserRepository();
        }

        public UserDto GetByEmailAndPass(string email, string password)
        {
            var user = _userRepo.GetUserAndApplication(u => u.EmailAddress == email && u.Password == password);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto GetByEmail(string email)
        {
            var user = _userRepo.GetUserAndApplication(u => u.EmailAddress == email);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }

        public List<UserDto> GetUsersAndApplications()
        {
            var students = _userRepo.GetUsersAndApplications(u => u.IsSystemAdmin == false && u.StudentApplication != null).ToList();
            var studentModels = Mapper.Map<List<UserDto>>(students);
            return studentModels;
        }

        public UserDto GetUserById(int id)
        {
            var student = _userRepo.GetUserAndApplication(u => u.Id == id);
            var studentModel = Mapper.Map<UserDto>(student);
            return studentModel;
        }

        public UserDto Add(UserDto user)
        {
            var entity = Mapper.Map<User>(user);
            var addedUser = _userRepo.Add(entity);
            var addedModel = Mapper.Map<UserDto>(addedUser);
            return addedModel;
        }

        public int Update(UserDto user)
        {
            var entity = Mapper.Map<User>(user);
            var updateUserResult = _userRepo.Update(entity);
            return updateUserResult;
        }

    }
}