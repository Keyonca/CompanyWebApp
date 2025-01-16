﻿namespace CompanyWebApp.Models
{
    //Класс DTO - предназначен для передачи на клмент (View, HTML),
    //скрывает за собой архитектуру реальной доменной модели
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? Description { get; set; }
        public string? PhotoFileName { get; set; }
        public string? Type { get; set; }
    }
}
