//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProgrammEasy
{
    using System;
    using System.Collections.Generic;
    
    public partial class Results
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdLesson { get; set; }
        public System.DateTime Date { get; set; }
        public int ScoreImg { get; set; }
        public string Description { get; set; }
    
        public virtual Lessons Lessons { get; set; }
        public virtual ScoreImage ScoreImage { get; set; }
        public virtual User User { get; set; }
    }
}
