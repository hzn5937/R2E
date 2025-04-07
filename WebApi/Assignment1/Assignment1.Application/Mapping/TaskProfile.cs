using Assignment1.Application.DTOs;
using Assignment1.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Application.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();
        }
    }
}
