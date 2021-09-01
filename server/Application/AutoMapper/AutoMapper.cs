namespace Application.AutoMapper
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Request.OrderStatusRequestDtos;
    using Application.DTO.Response;
    using Domain.Models;
    using global::AutoMapper;

    public static class AutoMapper
    {
        private static MapperConfiguration getPizzaDto;
        private static MapperConfiguration getPizzaFromCreateRequestDto;
        private static MapperConfiguration getPizzaFromUpdateRequestDto;
        private static MapperConfiguration getPizzaFromPartialUpdateRequestDto;

        private static MapperConfiguration getPizzaVariationDto;
        private static MapperConfiguration getPizzaVariationFromCreateRequestDto;
        private static MapperConfiguration getPizzaVariationFromUpdateRequestDto;
        private static MapperConfiguration getPizzaVariationFromPartialUpdateRequestDto;

        private static MapperConfiguration getIngredientDto;
        private static MapperConfiguration getIngredientFromIngredientDto;
        private static MapperConfiguration getIngredientFromCreateRequestDto;
        private static MapperConfiguration getIngredientFromUpdateRequestDto;
        private static MapperConfiguration getIngredientFromPartialUpdateRequestDto;

        private static MapperConfiguration getAdditionalIngredientDto;
        private static MapperConfiguration getAdditionalIngredientFromCreateRequestDto;
        private static MapperConfiguration getAdditionalIngredientFromUpdateRequestDto;
        private static MapperConfiguration getAdditionalIngredientFromPartialUpdateRequestDto;

        private static MapperConfiguration getDoughDto;
        private static MapperConfiguration getDoughFromCreateRequestDto;
        private static MapperConfiguration getDoughFromUpdateRequestDto;
        private static MapperConfiguration getDoughFromPartialUpdateRequestDto;

        private static MapperConfiguration getSizeDto;
        private static MapperConfiguration getSizeFromCreateRequestDto;
        private static MapperConfiguration getSizeFromUpdateRequestDto;
        private static MapperConfiguration getSizeFromPartialUpdateRequestDto;

        private static MapperConfiguration getOrderDto;
        private static MapperConfiguration getOrderFromCreateRequestDto;
        private static MapperConfiguration getOrderFromUpdateRequestDto;
        private static MapperConfiguration getOrderFromPatchRequestDto;

        private static MapperConfiguration getOrderLineDto;
        private static MapperConfiguration getOrderLineFromCreateRequestDto;
        private static MapperConfiguration getOrderLineFromUpdateRequestDto;
        private static MapperConfiguration getOrderLineFromPatchRequestDto;

        private static MapperConfiguration getOrderStatusDto;
        private static MapperConfiguration getOrderStatusFromCreateRequestDto;
        private static MapperConfiguration getOrderStatusFromUpdateRequestDto;
        private static MapperConfiguration getOrderStatusFromPatchRequestDto;

        static AutoMapper()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (getPizzaDto == null ||
                getPizzaFromCreateRequestDto == null ||
                getPizzaFromUpdateRequestDto == null ||
                getPizzaFromPartialUpdateRequestDto == null ||
                getPizzaVariationDto == null ||
                getPizzaVariationFromCreateRequestDto == null ||
                getPizzaVariationFromUpdateRequestDto == null ||
                getPizzaVariationFromPartialUpdateRequestDto == null ||
                getIngredientDto == null ||
                getIngredientFromIngredientDto == null ||
                getIngredientFromCreateRequestDto == null ||
                getIngredientFromUpdateRequestDto == null ||
                getIngredientFromPartialUpdateRequestDto == null ||
                getAdditionalIngredientDto == null ||
                getAdditionalIngredientFromCreateRequestDto == null ||
                getAdditionalIngredientFromUpdateRequestDto == null ||
                getAdditionalIngredientFromPartialUpdateRequestDto == null ||
                getSizeDto == null ||
                getSizeFromCreateRequestDto == null ||
                getSizeFromUpdateRequestDto == null ||
                getSizeFromPartialUpdateRequestDto == null ||
                getDoughDto == null ||
                getDoughFromCreateRequestDto == null ||
                getDoughFromUpdateRequestDto == null ||
                getDoughFromPartialUpdateRequestDto == null ||
                getOrderDto == null ||
                getOrderFromCreateRequestDto == null ||
                getOrderFromUpdateRequestDto == null ||
                getOrderFromPatchRequestDto == null ||
                getOrderLineDto == null ||
                getOrderLineFromCreateRequestDto == null ||
                getOrderLineFromUpdateRequestDto == null ||
                getOrderLineFromPatchRequestDto == null ||
                getOrderStatusDto == null ||
                getOrderStatusFromCreateRequestDto == null ||
                getOrderStatusFromUpdateRequestDto == null ||
                getOrderStatusFromPatchRequestDto == null)
            {
                getPizzaDto = new MapperConfiguration(cfg => cfg.CreateMap<Pizza, PizzaDto>()
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore()));
                getPizzaFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaCreateRequestDto, Pizza>());
                getPizzaFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaUpdateRequestDto, Pizza>()
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore()));
                getPizzaFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaPatchRequestDto, Pizza>()
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore()));

                getPizzaVariationDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaVariation, PizzaVariationDto>()
                .ForMember(dest => dest.Pizza, opts => opts.Ignore())
                .ForMember(dest => dest.Size, opts => opts.Ignore())
                .ForMember(dest => dest.Dough, opts => opts.Ignore())
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore())
                .ForMember(dest => dest.AdditionalIngredients, opts => opts.Ignore()));
                getPizzaVariationFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaVariationCreateRequestDto, PizzaVariation>()
                .ForMember(dest => dest.Pizza, opts => opts.Ignore())
                .ForMember(dest => dest.Size, opts => opts.Ignore())
                .ForMember(dest => dest.Dough, opts => opts.Ignore()));
                getPizzaVariationFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaVariationUpdateRequestDto, PizzaVariation>()
                .ForMember(dest => dest.Pizza, opts => opts.Ignore())
                .ForMember(dest => dest.Size, opts => opts.Ignore())
                .ForMember(dest => dest.Dough, opts => opts.Ignore())
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore())
                .ForMember(dest => dest.AdditionalIngredients, opts => opts.Ignore()));
                getPizzaVariationFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<PizzaPatchRequestDto, PizzaVariation>()
                .ForMember(dest => dest.Pizza, opts => opts.Ignore())
                .ForMember(dest => dest.Size, opts => opts.Ignore())
                .ForMember(dest => dest.Dough, opts => opts.Ignore())
                .ForMember(dest => dest.Ingredients, opts => opts.Ignore())
                .ForMember(dest => dest.AdditionalIngredients, opts => opts.Ignore()));

                getIngredientDto = new MapperConfiguration(cfg => cfg.CreateMap<Ingredient, IngredientDto>());
                getIngredientFromIngredientDto = new MapperConfiguration(cfg => cfg.CreateMap<IngredientDto, Ingredient>());
                getIngredientFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<IngredientCreateRequestDto, Ingredient>());
                getIngredientFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<IngredientUpdateRequestDto, Ingredient>());
                getIngredientFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<IngredientPatchRequestDto, Ingredient>());

                getAdditionalIngredientDto = new MapperConfiguration(cfg => cfg.CreateMap<AdditionalIngredient, AdditionalIngredientDto>());
                getAdditionalIngredientFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<AdditionalIngredientCreateRequestDto, AdditionalIngredient>());
                getAdditionalIngredientFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<AdditionalIngredientUpdateRequestDto, AdditionalIngredient>());
                getAdditionalIngredientFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<AdditionalIngredientPatchRequestDto, AdditionalIngredient>());

                getSizeDto = new MapperConfiguration(cfg => cfg.CreateMap<Size, SizeDto>());
                getSizeFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<SizeCreateRequestDto, Size>());
                getSizeFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<SizeUpdateRequestDto, Size>());
                getSizeFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<SizePatchRequestDto, Size>());

                getDoughDto = new MapperConfiguration(cfg => cfg.CreateMap<Dough, DoughDto>());
                getDoughFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<DoughCreateRequestDto, Dough>());
                getDoughFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<DoughUpdateRequestDto, Dough>());
                getDoughFromPartialUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<DoughPatchRequestDto, Dough>());

                getOrderDto = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderLines, opts => opts.Ignore())
                .ForMember(dest => dest.OrderStatus, opts => opts.Ignore())
                .ForMember(dest => dest.Date, opts => opts.Ignore()));
                getOrderFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderCreateRequestDto, Order>()
                .ForMember(dest => dest.OrderLines, opts => opts.Ignore())
                .ForMember(dest => dest.OrderStatus, opts => opts.Ignore()));
                getOrderFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderUpdateRequestDto, Order>()
                .ForMember(dest => dest.OrderLines, opts => opts.Ignore())
                .ForMember(dest => dest.OrderStatus, opts => opts.Ignore()));
                getOrderFromPatchRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderPatchRequestDto, Order>()
                .ForMember(dest => dest.OrderLines, opts => opts.Ignore())
                .ForMember(dest => dest.OrderStatus, opts => opts.Ignore()));

                getOrderLineDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderLine, OrderLineDto>()
                .ForMember(dest => dest.PizzaVariation, opts => opts.Ignore()));
                getOrderLineFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderLineCreateRequestDto, OrderLine>()
                .ForMember(dest => dest.PizzaVariation, opts => opts.Ignore()));
                getOrderLineFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderLineUpdateRequestDto, OrderLine>()
                .ForMember(dest => dest.PizzaVariation, opts => opts.Ignore()));
                getOrderLineFromPatchRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderLinePatchRequestDto, OrderLine>()
                .ForMember(dest => dest.PizzaVariation, opts => opts.Ignore()));

                getOrderStatusDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderStatus, OrderStatusDto>());
                getOrderStatusFromCreateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderStatusCreateRequestDto, OrderStatus>()
                .ForMember(dest => dest.Orders, opts => opts.Ignore()));
                getOrderStatusFromUpdateRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderStatusUpdateRequestDto, OrderStatus>()
                .ForMember(dest => dest.Orders, opts => opts.Ignore()));
                getOrderStatusFromPatchRequestDto = new MapperConfiguration(cfg => cfg.CreateMap<OrderStatusPatchRequestDto, OrderStatus>()
                .ForMember(dest => dest.Orders, opts => opts.Ignore()));
            }
        }

        public static PizzaDto ToViewModel(this Pizza pizza)
        {
            PizzaDto pizzaDto = new Mapper(getPizzaDto).Map<PizzaDto>(pizza);

            pizzaDto.Ingredients = new List<IngredientDto>();

            pizzaDto.Ingredients = pizza.Ingredients.Select(ing => ing.ToViewModel()).ToList();

            return pizzaDto;
        }

        public static Pizza ToModel(this PizzaCreateRequestDto pizzaCreateRequestDto)
        {
            Pizza pizza = new Mapper(getPizzaFromCreateRequestDto).Map<Pizza>(pizzaCreateRequestDto);
            pizza.Ingredients = new List<Ingredient>();

            return pizza;
        }

        public static Pizza ToModel(this PizzaUpdateRequestDto pizzaUpdateRequestDto)
        {
            Pizza pizza = new Mapper(getPizzaFromUpdateRequestDto).Map<Pizza>(pizzaUpdateRequestDto);
            pizza.Ingredients = new List<Ingredient>();

            return pizza;
        }

        public static Pizza ToModel(this PizzaPatchRequestDto pizzaPartialUpdateRequestDto)
        {
            Pizza pizza = new Mapper(getPizzaFromPartialUpdateRequestDto).Map<Pizza>(pizzaPartialUpdateRequestDto);
            pizza.Ingredients = new List<Ingredient>();

            return pizza;
        }

        public static PizzaVariationDto ToViewModel(this PizzaVariation pizzaVariation)
        {
            PizzaVariationDto pizzaVariationDto = new Mapper(getPizzaVariationDto).Map<PizzaVariationDto>(pizzaVariation);

            pizzaVariationDto.Pizza = pizzaVariation.Pizza.ToViewModel();

            pizzaVariationDto.Size = pizzaVariation.Size.ToViewModel();

            pizzaVariationDto.Dough = pizzaVariation.Dough.ToViewModel();

            pizzaVariationDto.Ingredients = pizzaVariation.Ingredients.Select(ing => ing.ToViewModel()).ToList();

            pizzaVariationDto.AdditionalIngredients = pizzaVariation.AdditionalIngredients.Select(ing => ing.ToViewModel()).ToList();

            return pizzaVariationDto;
        }

        public static PizzaVariation ToModel(this PizzaVariationCreateRequestDto pizzaVariationCreateRequestDto)
        {
            PizzaVariation pizzaVariation = new Mapper(getPizzaVariationFromCreateRequestDto).Map<PizzaVariation>(pizzaVariationCreateRequestDto);
            pizzaVariation.Ingredients = new List<Ingredient>();
            pizzaVariation.AdditionalIngredients = new List<AdditionalIngredient>();

            return pizzaVariation;
        }

        public static PizzaVariation ToModel(this PizzaVariationUpdateRequestDto pizzaVariationUpdateRequestDto)
        {
            PizzaVariation pizzaVariation = new Mapper(getPizzaVariationFromUpdateRequestDto).Map<PizzaVariation>(pizzaVariationUpdateRequestDto);
            pizzaVariation.Ingredients = new List<Ingredient>();
            pizzaVariation.AdditionalIngredients = new List<AdditionalIngredient>();

            return pizzaVariation;
        }

        public static PizzaVariation ToModel(this PizzaVariationPatchRequestDto pizzaVariationPartialUpdateRequestDto)
        {
            PizzaVariation pizzaVariation = new PizzaVariation();
            pizzaVariation.Ingredients = new List<Ingredient>();
            pizzaVariation.AdditionalIngredients = new List<AdditionalIngredient>();

            return pizzaVariation;
        }

        public static IngredientDto ToViewModel(this Ingredient ingredient)
        {
            return new Mapper(getIngredientDto).Map<IngredientDto>(ingredient);
        }

        public static Ingredient ToModel(this IngredientCreateRequestDto ingredientCreateRequestDto)
        {
            Ingredient ingredient = new Mapper(getIngredientFromCreateRequestDto).Map<Ingredient>(ingredientCreateRequestDto);
            ingredient.Pizzas = new List<Pizza>();

            return ingredient;
        }

        public static Ingredient ToModel(this IngredientDto ingredientDto)
        {
            Ingredient ingredient = new Mapper(getIngredientFromIngredientDto).Map<Ingredient>(ingredientDto);
            ingredient.Pizzas = new List<Pizza>();

            return ingredient;
        }

        public static Ingredient ToModel(this IngredientUpdateRequestDto ingredientUpdateRequestDto)
        {
            return new Mapper(getIngredientFromUpdateRequestDto).Map<Ingredient>(ingredientUpdateRequestDto);
        }

        public static Ingredient ToModel(this IngredientPatchRequestDto ingredientPartialUpdateRequestDto)
        {
            return new Mapper(getIngredientFromPartialUpdateRequestDto).Map<Ingredient>(ingredientPartialUpdateRequestDto);
        }

        public static AdditionalIngredientDto ToViewModel(this AdditionalIngredient additionalIngredient)
        {
            return new Mapper(getAdditionalIngredientDto).Map<AdditionalIngredientDto>(additionalIngredient);
        }

        public static AdditionalIngredient ToModel(this AdditionalIngredientCreateRequestDto additionalIngredientCreateRequestDto)
        {
            AdditionalIngredient additionalIngredient = new Mapper(getAdditionalIngredientFromCreateRequestDto).Map<AdditionalIngredient>(additionalIngredientCreateRequestDto);
            additionalIngredient.PizzasVariations = new List<PizzaVariation>();

            return additionalIngredient;
        }

        public static AdditionalIngredient ToModel(this AdditionalIngredientUpdateRequestDto additionalIngredientUpdateRequestDto)
        {
            return new Mapper(getAdditionalIngredientFromUpdateRequestDto).Map<AdditionalIngredient>(additionalIngredientUpdateRequestDto);
        }

        public static AdditionalIngredient ToModel(this AdditionalIngredientPatchRequestDto additionalIngredientPartialUpdateRequestDto)
        {
            return new Mapper(getAdditionalIngredientFromPartialUpdateRequestDto).Map<AdditionalIngredient>(additionalIngredientPartialUpdateRequestDto);
        }

        public static DoughDto ToViewModel(this Dough dough)
        {
            return new Mapper(getDoughDto).Map<DoughDto>(dough);
        }

        public static Dough ToModel(this DoughCreateRequestDto doughCreateRequestDto)
        {
            Dough dough = new Mapper(getDoughFromCreateRequestDto).Map<Dough>(doughCreateRequestDto);
            dough.PizzaVariations = new List<PizzaVariation>();

            return dough;
        }

        public static Dough ToModel(this DoughUpdateRequestDto doughUpdateRequestDto)
        {
            return new Mapper(getDoughFromUpdateRequestDto).Map<Dough>(doughUpdateRequestDto);
        }

        public static Dough ToModel(this DoughPatchRequestDto doughPartialUpdateRequestDto)
        {
            return new Mapper(getDoughFromPartialUpdateRequestDto).Map<Dough>(doughPartialUpdateRequestDto);
        }

        public static SizeDto ToViewModel(this Size size)
        {
            return new Mapper(getSizeDto).Map<SizeDto>(size);
        }

        public static Size ToModel(this SizeCreateRequestDto sizeCreateRequestDto)
        {
            Size size = new Mapper(getSizeFromCreateRequestDto).Map<Size>(sizeCreateRequestDto);
            size.PizzaVariations = new List<PizzaVariation>();

            return size;
        }

        public static Size ToModel(this SizeUpdateRequestDto sizeUpdateRequestDto)
        {
            return new Mapper(getSizeFromUpdateRequestDto).Map<Size>(sizeUpdateRequestDto);
        }

        public static Size ToModel(this SizePatchRequestDto sizePartialUpdateRequestDto)
        {
            return new Mapper(getSizeFromPartialUpdateRequestDto).Map<Size>(sizePartialUpdateRequestDto);
        }

        public static OrderDto ToViewModel(this Order order)
        {
            OrderDto orderDto = new Mapper(getOrderDto).Map<OrderDto>(order);
            orderDto.OrderLines = new List<OrderLineDto>();
            orderDto.OrderLines = order.OrderLines.Select(line => line.ToViewModel()).ToList();
            orderDto.OrderStatus = order.OrderStatus.ToViewModel();

            return orderDto;
        }

        public static Order ToModel(this OrderCreateRequestDto orderCreateRequestDto)
        {
            Order order = new Mapper(getOrderFromCreateRequestDto).Map<Order>(orderCreateRequestDto);
            order.OrderStatus = new OrderStatus();
            order.OrderLines = new List<OrderLine>();

            return order;
        }

        public static Order ToModel(this OrderUpdateRequestDto orderUpdateRequestDto)
        {
            Order order = new Mapper(getOrderFromUpdateRequestDto).Map<Order>(orderUpdateRequestDto);
            order.OrderStatus = new OrderStatus();
            order.OrderLines = new List<OrderLine>();

            return order;
        }

        public static Order ToModel(this OrderPatchRequestDto orderPatchRequestDto)
        {
            Order order = new Mapper(getOrderFromPatchRequestDto).Map<Order>(orderPatchRequestDto);
            order.OrderStatus = new OrderStatus();
            order.OrderLines = new List<OrderLine>();

            return order;
        }

        public static OrderLineDto ToViewModel(this OrderLine orderLine)
        {
            OrderLineDto orderLineDto = new Mapper(getOrderLineDto).Map<OrderLineDto>(orderLine);
            orderLineDto.PizzaVariation = new PizzaVariationDto();

            return orderLineDto;
        }

        public static OrderLine ToModel(this OrderLineCreateRequestDto orderLineCreateRequestDto)
        {
            OrderLine orderLine = new Mapper(getOrderLineFromCreateRequestDto).Map<OrderLine>(orderLineCreateRequestDto);
            orderLine.PizzaVariation = new PizzaVariation();

            return orderLine;
        }

        public static OrderLine ToModel(this OrderLineUpdateRequestDto orderLineUpdateRequestDto)
        {
            OrderLine orderLine = new Mapper(getOrderLineFromUpdateRequestDto).Map<OrderLine>(orderLineUpdateRequestDto);
            orderLine.PizzaVariation = new PizzaVariation();

            return orderLine;
        }

        public static OrderLine ToModel(this OrderLinePatchRequestDto orderLinePatchRequestDto)
        {
            OrderLine orderLine = new Mapper(getOrderLineFromPatchRequestDto).Map<OrderLine>(orderLinePatchRequestDto);
            orderLine.PizzaVariation = new PizzaVariation();

            return orderLine;
        }

        public static OrderStatusDto ToViewModel(this OrderStatus orderStatus)
        {
            OrderStatusDto orderStatusDto = new Mapper(getOrderStatusDto).Map<OrderStatusDto>(orderStatus);

            return orderStatusDto;
        }

        public static OrderStatus ToModel(this OrderStatusCreateRequestDto orderStatusCreateRequestDto)
        {
            OrderStatus orderStatus = new Mapper(getOrderStatusFromCreateRequestDto).Map<OrderStatus>(orderStatusCreateRequestDto);
            orderStatus.Orders = new List<Order>();

            return orderStatus;
        }

        public static OrderStatus ToModel(this OrderStatusUpdateRequestDto orderStatusUpdateRequestDto)
        {
            OrderStatus orderStatus = new Mapper(getOrderStatusFromUpdateRequestDto).Map<OrderStatus>(orderStatusUpdateRequestDto);
            orderStatus.Orders = new List<Order>();

            return orderStatus;
        }

        public static OrderStatus ToModel(this OrderStatusPatchRequestDto orderStatusPatchRequestDto)
        {
            OrderStatus orderStatus = new Mapper(getOrderStatusFromPatchRequestDto).Map<OrderStatus>(orderStatusPatchRequestDto);
            orderStatus.Orders = new List<Order>();

            return orderStatus;
        }
    }
}
