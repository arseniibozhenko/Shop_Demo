using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Shop_Demo.BLL.DTO;
using Shop_Demo.BLL.Mapping;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.BLL.Services
{
    public class PhotosService : IEntityService<PhotoDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public PhotosService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            //Настройка AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddExpressionMapping();
                cfg.AddProfile<ModelToDTOProfile>();
                cfg.AddProfile<DTOToModelProfile>();
            });
            mapper = config.CreateMapper();
        }

        public List<PhotoDTO> GetAll()
        {
            List<Photo> photos = unitOfWork.photosRepository.GetAll();
            //List<PhotoDTO> photosDTO = mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            List<PhotoDTO> photosDTO = photos.Select(g => mapper.Map<PhotoDTO>(g)).ToList();

            return photosDTO;
        }

        public PhotoDTO GetById(int id)
        {
            Photo photo = unitOfWork.photosRepository.GetById(id);
            PhotoDTO photoDTO = mapper.Map<PhotoDTO>(photo);

            return photoDTO;
        }

        public List<PhotoDTO> GetBy(Expression<Func<PhotoDTO, bool>> predicate)
        {
            Expression<Func<Photo, bool>> p = mapper.Map<Expression<Func<Photo, bool>>>(predicate);
            List<Photo> photos = unitOfWork.photosRepository.GetBy(p);
            //List<PhotoDTO> photosDTO = mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            List<PhotoDTO> photosDTO = photos.Select(g => mapper.Map<PhotoDTO>(g)).ToList();

            return photosDTO;
        }

        public void CreateOrUpdate(PhotoDTO item)
        {
            Photo photo = mapper.Map<Photo>(item);
            unitOfWork.photosRepository.CreateOrUpdate(photo);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.photosRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(PhotoDTO item)
        {
            Photo photo = mapper.Map<Photo>(item);
            unitOfWork.photosRepository.Delete(photo);
            unitOfWork.Save();
        }
    }
}
