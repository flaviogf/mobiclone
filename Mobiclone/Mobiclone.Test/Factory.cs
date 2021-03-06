﻿using Bogus;
using Mobiclone.Api.Lib;
using Mobiclone.Api.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mobiclone.Test
{
    public class Factory
    {
        private static readonly Faker _faker = new Faker();

        private static readonly IHash _hash = new Bcrypt();

        public async static Task<User> User(string name = null, string email = null, string password = null, int? fileId = null)
        {
            var _name = name ?? _faker.Person.FirstName;
            var _email = email ?? _faker.Person.Email;
            var _password = password ?? _faker.Internet.Password();

            var user = new User
            {
                Name = _name,
                Email = _email,
                PasswordHash = await _hash.Make(_password),
                FileId = fileId
            };

            return user;
        }

        public async static Task<Account> Account(string name = null, string type = null, int? userId = null)
        {
            var _name = name ?? _faker.Finance.Account();
            var _type = type ?? _faker.Finance.AccountName();
            var _userId = userId ?? 1;

            var account = new Account
            {
                Name = _name,
                Type = _type,
                UserId = _userId
            };

            return account;
        }

        public async static Task<Revenue> Revenue(string description = null, int? value = null, DateTime? date = null, int? accountId = null)
        {
            var _description = description ?? _faker.Lorem.Sentence();
            var _value = value ?? _faker.Random.Int();
            var _date = date ?? _faker.Date.Recent();
            var _accountId = accountId ?? 1;

            var revenue = new Revenue
            {
                Description = _description,
                Value = _value,
                Date = _date,
                AccountId = _accountId
            };

            return revenue;
        }

        public async static Task<Expense> Expense(string description = null, int? value = null, DateTime? date = null, int? accountId = null)
        {
            var _description = description ?? _faker.Lorem.Sentence();
            var _value = value ?? _faker.Random.Int();
            var _date = date ?? _faker.Date.Recent();
            var _accountId = accountId ?? 1;

            var expense = new Expense
            {
                Description = _description,
                Value = _value,
                Date = _date,
                AccountId = _accountId
            };

            return expense;
        }

        public async static Task<ClaimsPrincipal> ClaimsPrincipal(int userId)
        {
            var claimsPrincipal = new ClaimsPrincipal
            (
                new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    }
                )
            );

            return claimsPrincipal;
        }

        public static async Task<File> File(string name = null, string path = null)
        {
            var _name = name ?? _faker.Internet.UserName();

            var _path = path ?? _faker.Internet.Url();

            var file = new File
            {
                Name = _name,
                Path = _path
            };

            return file;
        }

        public static async Task<Output> Output(int toId, int fromId, string description = null, DateTime? date = null, int? value = null)
        {
            var _description = description ?? _faker.Lorem.Sentence();
            var _date = date ?? DateTime.Now;
            var _value = value ?? _faker.Random.Int();

            var output = new Output
            {
                Description = _description,
                Date = _date,
                Value = _value,
                ToId = toId,
                FromId = fromId
            };

            return output;
        }

        public static async Task<Input> Input(int toId, int fromId, string description = null, DateTime? date = null, int? value = null)
        {
            var _description = description ?? _faker.Lorem.Sentence();
            var _date = date ?? DateTime.Now;
            var _value = value ?? _faker.Random.Int();

            var input = new Input
            {
                Description = _description,
                Date = _date,
                Value = _value,
                ToId = toId,
                FromId = fromId
            };

            return input;
        }
    }
}
