namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class SizeService : ISizeService
    {
        private ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public void Delete(string id)
        {
            _sizeRepository.Delete(id);
        }

        public SizeDto GetByName(string name)
        {
            var existingSize = _sizeRepository.GetByName(name);

            if (existingSize != null)
            {
                return existingSize.ToViewModel();
            }

            return null;
        }

        public SizeDto GetById(string id)
        {
            var existingSize = _sizeRepository.GetById(id);

            if (existingSize != null)
            {
                return existingSize.ToViewModel();
            }

            return null;
        }

        public IEnumerable<SizeDto> GetAll()
        {
            return _sizeRepository.GetAll().Select(x => x.ToViewModel());
        }

        public SizeDto Insert(SizeCreateRequestDto item)
        {
            return _sizeRepository.Insert(item.ToModel()).ToViewModel();
        }

        public SizeDto Update(string id, SizeUpdateRequestDto item)
        {
            return _sizeRepository.Update(id, item.ToModel()).ToViewModel();
        }

        public SizeDto Patch(string id, SizePatchRequestDto size)
        {
            return _sizeRepository.Patch(id, size.ToModel()).ToViewModel();
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _sizeRepository.GetIdentificators();
        }
    }
}