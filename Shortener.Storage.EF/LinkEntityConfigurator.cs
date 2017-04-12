using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Shortener.Datas;

namespace Shortener.Storage.EF
{
    public class LinkEntityConfigurator : IEntityConfigurator
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            var ec = modelBuilder.Entity<Link>();
            ec.ToTable("Links");
            ec.HasKey(p => p.Id);
            ec.Property(p => p.Url).IsRequired();
            ec.Property(p => p.UserId)
              .IsRequired()
              .HasColumnAnnotation(
                  IndexAnnotation.AnnotationName,
                  new IndexAnnotation(new IndexAttribute("IX_UserId", 1)));
            ec.Property(p => p.Key)
              .IsRequired()
              .HasMaxLength(20)
              .HasColumnAnnotation(
                  IndexAnnotation.AnnotationName,
                  new IndexAnnotation(new IndexAttribute("IX_Key", 1) {IsUnique = true}));
            ;
        }
    }
}