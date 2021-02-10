using System;

namespace Nytte.Sample.ModuleB.Dtos
{
    public class UserDto
    {
        public Guid Id { get; }
        
        public string Username { get; }

        public UserDto(Guid id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}