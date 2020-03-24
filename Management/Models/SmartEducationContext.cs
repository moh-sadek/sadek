using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Management.Models
{
    public partial class SmartEducationContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Chapter> Chapter { get; set; }
        public virtual DbSet<EducationLevel> EducationLevel { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<ExamType> ExamType { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<Municipality> Municipality { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolType> SchoolType { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentExams> StudentExams { get; set; }
        public virtual DbSet<StudentUser> StudentUser { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<TakenExam> TakenExam { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubject { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLevel> UserLevel { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VideoAttachment> VideoAttachment { get; set; }

        public SmartEducationContext(DbContextOptions<SmartEducationContext> options) : base(options) { }

        // Unable to generate entity type for table 'dbo.StudentAnsewr'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.EducationMinister'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.StudentReport'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SubjectEnrollment'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=LAPTOP-DVJT5BST;database=SmartEducation;uid=Ahmed;pwd=35087124567Ahmed;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExamAnswer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QuestionId).HasColumnName("questionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LectureId).HasColumnName("lectureID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachment_Lecture");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Sequance).HasColumnName("sequance");

                entity.Property(e => e.SubjectId).HasColumnName("subjectID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Chapter)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chapter_Subject");
            });

            modelBuilder.Entity<EducationLevel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.ExamType).HasColumnName("examType");

                entity.Property(e => e.ExamTypeId).HasColumnName("examTypeID");

                entity.Property(e => e.GradeId).HasColumnName("gradeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SubjectId).HasColumnName("subjectID");

                entity.Property(e => e.TeacherSubjectId).HasColumnName("teacherSubjectID");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.ExamTypeNavigation)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.ExamTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_ExamType");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_Subject");

                entity.HasOne(d => d.TeacherSubject)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.TeacherSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_TeacherSubject");
            });

            modelBuilder.Entity<ExamType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChapterId).HasColumnName("chapterID");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Sequance).HasColumnName("sequance");

                entity.Property(e => e.SubjectId).HasColumnName("subjectID");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lecture_Chapter1");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lecture_Subject1");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnswerId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExamId).HasColumnName("examID");

                entity.Property(e => e.Question1)
                    .IsRequired()
                    .HasColumnName("question")
                    .HasMaxLength(50);

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Exam");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.EducationLevelId).HasColumnName("educationLevelID");

                entity.Property(e => e.MunicipalityId).HasColumnName("municipalityID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SchoolTypeId).HasColumnName("schoolTypeID");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.EducationLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_School_EducationLevel");

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_School_Municipality");

                entity.HasOne(d => d.SchoolType)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.SchoolTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_School_SchoolType");
            });

            modelBuilder.Entity<SchoolType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.GradeId).HasColumnName("gradeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdPassport)
                    .IsRequired()
                    .HasColumnName("nationalID/passport")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolId).HasColumnName("schoolID");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Grade");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_School");
            });

            modelBuilder.Entity<StudentExams>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.StudentExams)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_StudentExams_Exam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExams)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentExams_Student");
            });

            modelBuilder.Entity<StudentUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.HasOne(d => d.LevelNavigation)
                    .WithMany(p => p.StudentUser)
                    .HasForeignKey(d => d.Level)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentUser_UserLevel");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentUser)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentUser_Student");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.GradeId).HasColumnName("gradeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subject_Grade");
            });

            modelBuilder.Entity<TakenExam>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer")
                    .HasMaxLength(50);

                entity.Property(e => e.AnswerId).HasColumnName("answerID");

                entity.Property(e => e.ExamId).HasColumnName("examID");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.HasOne(d => d.AnswerNavigation)
                    .WithMany(p => p.TakenExam)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TakenExam_Answer");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.TakenExam)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TakenExam_Exam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TakenExam)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TakenExam_Student");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.NationalIdPassport)
                    .HasColumnName("nationalID/passport")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.SchoolId).HasColumnName("schoolID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Teacher_School");
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasColumnName("class")
                    .HasMaxLength(50);

                entity.Property(e => e.GradeId).HasColumnName("gradeID");

                entity.Property(e => e.SubjectId).HasColumnName("subjectID");

                entity.Property(e => e.TeacherId).HasColumnName("teacherID");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_TeacherSubject_Grade");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubject_Subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubject_Teacher");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.LevelNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Level)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserLevel");
            });

            modelBuilder.Entity<UserLevel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastLoginOn).HasColumnType("datetime");

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.LoginTryAttemptDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.State).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserType).HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<VideoAttachment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LectureId).HasColumnName("lectureID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.VideoAttachment)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoAttachment_Lecture");
            });
        }
    }
}
