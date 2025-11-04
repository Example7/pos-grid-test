using System;
using System.Collections.Generic;
using DevExpress.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Data;

public partial class SupabaseDbContext : DbContext
{
    public SupabaseDbContext()
    {
    }

    public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspnetApplication> AspnetApplications { get; set; }

    public virtual DbSet<AspnetMembership> AspnetMemberships { get; set; }

    public virtual DbSet<AspnetPath> AspnetPaths { get; set; }

    public virtual DbSet<AspnetPersonalizationalluser> AspnetPersonalizationallusers { get; set; }

    public virtual DbSet<AspnetPersonalizationperuser> AspnetPersonalizationperusers { get; set; }

    public virtual DbSet<AspnetProfile> AspnetProfiles { get; set; }

    public virtual DbSet<AspnetRole> AspnetRoles { get; set; }

    public virtual DbSet<AspnetSchemaversion> AspnetSchemaversions { get; set; }

    public virtual DbSet<AspnetUser> AspnetUsers { get; set; }

    public virtual DbSet<AspnetWebeventEvent> AspnetWebeventEvents { get; set; }

    public virtual DbSet<Contractor> Contractors { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountsProduct> DiscountsProducts { get; set; }

    public virtual DbSet<DiscountsStore> DiscountsStores { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FinancialDocumentsType> FinancialDocumentsTypes { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<IncomesItem> IncomesItems { get; set; }

    public virtual DbSet<IncomesStatus> IncomesStatuses { get; set; }

    public virtual DbSet<Inorder> Inorders { get; set; }

    public virtual DbSet<InordersItem> InordersItems { get; set; }

    public virtual DbSet<LoyaltiesWallet> LoyaltiesWallets { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersInvoice> OrdersInvoices { get; set; }

    public virtual DbSet<OrdersInvoicesType> OrdersInvoicesTypes { get; set; }

    public virtual DbSet<OrdersItem> OrdersItems { get; set; }

    public virtual DbSet<OrdersItemsOperationsLog> OrdersItemsOperationsLogs { get; set; }

    public virtual DbSet<OrdersOperationsLog> OrdersOperationsLogs { get; set; }

    public virtual DbSet<OrdersPaymentsStatus> OrdersPaymentsStatuses { get; set; }

    public virtual DbSet<OrdersRealizationsStatus> OrdersRealizationsStatuses { get; set; }

    public virtual DbSet<OrdersRealizationsType> OrdersRealizationsTypes { get; set; }

    public virtual DbSet<OrdersRealizationsTypesStatusesPath> OrdersRealizationsTypesStatusesPaths { get; set; }

    public virtual DbSet<OrdersVatSummary> OrdersVatSummaries { get; set; }

    public virtual DbSet<Outcome> Outcomes { get; set; }

    public virtual DbSet<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; }

    public virtual DbSet<OutcomesFinancialDocumentsItem> OutcomesFinancialDocumentsItems { get; set; }

    public virtual DbSet<OutcomesFinancialDocumentsStatus> OutcomesFinancialDocumentsStatuses { get; set; }

    public virtual DbSet<OutcomesFinancialDocumentsVatSummary> OutcomesFinancialDocumentsVatSummaries { get; set; }

    public virtual DbSet<OutcomesItem> OutcomesItems { get; set; }

    public virtual DbSet<OutcomesStatus> OutcomesStatuses { get; set; }

    public virtual DbSet<PaymentsMethod> PaymentsMethods { get; set; }

    public virtual DbSet<Pose> Poses { get; set; }

    public virtual DbSet<PosesType> PosesTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSetsItem> ProductSetsItems { get; set; }

    public virtual DbSet<ProductsBarcode> ProductsBarcodes { get; set; }

    public virtual DbSet<ProductsCategories1> ProductsCategories1s { get; set; }

    public virtual DbSet<ProductsCategories2> ProductsCategories2s { get; set; }

    public virtual DbSet<ProductsQuantityUnit> ProductsQuantityUnits { get; set; }

    public virtual DbSet<ProductsRecipe> ProductsRecipes { get; set; }

    public virtual DbSet<ProductsRecipesItem> ProductsRecipesItems { get; set; }

    public virtual DbSet<ProductsVatRate> ProductsVatRates { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<SetsCategory> SetsCategories { get; set; }

    public virtual DbSet<SetsItem> SetsItems { get; set; }

    public virtual DbSet<SetsItemsProduct> SetsItemsProducts { get; set; }

    public virtual DbSet<SetsSchedule> SetsSchedules { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopsSet> ShopsSets { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockHistory> StockHistories { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoresDocumentsType> StoresDocumentsTypes { get; set; }

    public virtual DbSet<StoresDocumentsTypesCategory> StoresDocumentsTypesCategories { get; set; }

    public virtual DbSet<StoresOrdersType> StoresOrdersTypes { get; set; }

    public virtual DbSet<SyncVersion> SyncVersions { get; set; }

    public virtual DbSet<UserContext> UserContexts { get; set; }

    public virtual DbSet<Year> Years { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=aws-1-eu-north-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.ianqgvcvleyondiyhdjd;Password=fonjen-dakxow-wyxRe3;SslMode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<AspnetApplication>(entity =>
        {
            entity.HasKey(e => e.Applicationid).HasName("aspnet_applications_pkey");

            entity.ToTable("aspnet_applications");

            entity.Property(e => e.Applicationid)
                .ValueGeneratedNever()
                .HasColumnName("applicationid");
            entity.Property(e => e.Applicationname).HasColumnName("applicationname");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Loweredapplicationname).HasColumnName("loweredapplicationname");
        });

        modelBuilder.Entity<AspnetMembership>(entity =>
        {
            entity.HasKey(e => new { e.Applicationid, e.Userid }).HasName("pk_aspnet_membership");

            entity.ToTable("aspnet_membership");

            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Createdate).HasColumnName("createdate");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Failedpasswordanswerattemptcount).HasColumnName("failedpasswordanswerattemptcount");
            entity.Property(e => e.Failedpasswordanswerattemptwindowstart).HasColumnName("failedpasswordanswerattemptwindowstart");
            entity.Property(e => e.Failedpasswordattemptcount).HasColumnName("failedpasswordattemptcount");
            entity.Property(e => e.Failedpasswordattemptwindowstart).HasColumnName("failedpasswordattemptwindowstart");
            entity.Property(e => e.Isapproved).HasColumnName("isapproved");
            entity.Property(e => e.Islockedout).HasColumnName("islockedout");
            entity.Property(e => e.Lastlockoutdate).HasColumnName("lastlockoutdate");
            entity.Property(e => e.Lastlogindate).HasColumnName("lastlogindate");
            entity.Property(e => e.Lastpasswordchangeddate).HasColumnName("lastpasswordchangeddate");
            entity.Property(e => e.Loweredemail).HasColumnName("loweredemail");
            entity.Property(e => e.Mobilepin).HasColumnName("mobilepin");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Passwordanswer).HasColumnName("passwordanswer");
            entity.Property(e => e.Passwordformat).HasColumnName("passwordformat");
            entity.Property(e => e.Passwordquestion).HasColumnName("passwordquestion");
            entity.Property(e => e.Passwordsalt).HasColumnName("passwordsalt");

            entity.HasOne(d => d.Application).WithMany(p => p.AspnetMemberships)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_membership_applicationid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AspnetMemberships)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_membership_userid_fkey");
        });

        modelBuilder.Entity<AspnetPath>(entity =>
        {
            entity.HasKey(e => e.Pathid).HasName("aspnet_paths_pkey");

            entity.ToTable("aspnet_paths");

            entity.Property(e => e.Pathid)
                .ValueGeneratedNever()
                .HasColumnName("pathid");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Loweredpath).HasColumnName("loweredpath");
            entity.Property(e => e.Path).HasColumnName("path");

            entity.HasOne(d => d.Application).WithMany(p => p.AspnetPaths)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_paths_applicationid_fkey");
        });

        modelBuilder.Entity<AspnetPersonalizationalluser>(entity =>
        {
            entity.HasKey(e => e.Pathid).HasName("pk_aspnet_personalizationallusers");

            entity.ToTable("aspnet_personalizationallusers");

            entity.Property(e => e.Pathid)
                .ValueGeneratedNever()
                .HasColumnName("pathid");
            entity.Property(e => e.Lastupdateddate).HasColumnName("lastupdateddate");
            entity.Property(e => e.Pagesettings).HasColumnName("pagesettings");

            entity.HasOne(d => d.Path).WithOne(p => p.AspnetPersonalizationalluser)
                .HasForeignKey<AspnetPersonalizationalluser>(d => d.Pathid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_personalizationallusers_pathid_fkey");
        });

        modelBuilder.Entity<AspnetPersonalizationperuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aspnet_personalizationperuser_pkey");

            entity.ToTable("aspnet_personalizationperuser");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Lastupdateddate).HasColumnName("lastupdateddate");
            entity.Property(e => e.Pagesettings).HasColumnName("pagesettings");
            entity.Property(e => e.Pathid).HasColumnName("pathid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Path).WithMany(p => p.AspnetPersonalizationperusers)
                .HasForeignKey(d => d.Pathid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_personalizationperuser_pathid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AspnetPersonalizationperusers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_personalizationperuser_userid_fkey");
        });

        modelBuilder.Entity<AspnetProfile>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("pk_aspnet_profile");

            entity.ToTable("aspnet_profile");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Lastupdateddate).HasColumnName("lastupdateddate");
            entity.Property(e => e.Propertynames).HasColumnName("propertynames");
            entity.Property(e => e.Propertyvaluesbinary).HasColumnName("propertyvaluesbinary");
            entity.Property(e => e.Propertyvaluesstring).HasColumnName("propertyvaluesstring");

            entity.HasOne(d => d.User).WithOne(p => p.AspnetProfile)
                .HasForeignKey<AspnetProfile>(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_profile_userid_fkey");
        });

        modelBuilder.Entity<AspnetRole>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("aspnet_roles_pkey");

            entity.ToTable("aspnet_roles");

            entity.Property(e => e.Roleid)
                .ValueGeneratedNever()
                .HasColumnName("roleid");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Loweredrolename).HasColumnName("loweredrolename");
            entity.Property(e => e.Rolename).HasColumnName("rolename");

            entity.HasOne(d => d.Application).WithMany(p => p.AspnetRoles)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_roles_applicationid_fkey");
        });

        modelBuilder.Entity<AspnetSchemaversion>(entity =>
        {
            entity.HasKey(e => e.Feature).HasName("pk_aspnet_schemaversions");

            entity.ToTable("aspnet_schemaversions");

            entity.Property(e => e.Feature).HasColumnName("feature");
            entity.Property(e => e.Compatibleschemaversion).HasColumnName("compatibleschemaversion");
            entity.Property(e => e.Iscurrentversion).HasColumnName("iscurrentversion");
        });

        modelBuilder.Entity<AspnetUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("aspnet_users_pkey");

            entity.ToTable("aspnet_users");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.EmplyeeId).HasColumnName("emplyee_id");
            entity.Property(e => e.Isanonymous)
                .HasDefaultValue(false)
                .HasColumnName("isanonymous");
            entity.Property(e => e.Lastactivitydate).HasColumnName("lastactivitydate");
            entity.Property(e => e.Loweredusername).HasColumnName("loweredusername");
            entity.Property(e => e.Mobilealias).HasColumnName("mobilealias");
            entity.Property(e => e.Userlogo).HasColumnName("userlogo");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Application).WithMany(p => p.AspnetUsers)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aspnet_users_applicationid_fkey");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspnetUsersinrole",
                    r => r.HasOne<AspnetRole>().WithMany()
                        .HasForeignKey("Roleid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("aspnet_usersinroles_roleid_fkey"),
                    l => l.HasOne<AspnetUser>().WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("aspnet_usersinroles_userid_fkey"),
                    j =>
                    {
                        j.HasKey("Userid", "Roleid").HasName("aspnet_usersinroles_pkey");
                        j.ToTable("aspnet_usersinroles");
                        j.IndexerProperty<Guid>("Userid").HasColumnName("userid");
                        j.IndexerProperty<Guid>("Roleid").HasColumnName("roleid");
                    });
        });

        modelBuilder.Entity<AspnetWebeventEvent>(entity =>
        {
            entity.HasKey(e => e.Eventid).HasName("aspnet_webevent_events_pkey");

            entity.ToTable("aspnet_webevent_events");

            entity.Property(e => e.Eventid)
                .ValueGeneratedNever()
                .HasColumnName("eventid");
            entity.Property(e => e.Applicationpath).HasColumnName("applicationpath");
            entity.Property(e => e.Applicationvirtualpath).HasColumnName("applicationvirtualpath");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Eventcode).HasColumnName("eventcode");
            entity.Property(e => e.Eventdetailcode).HasColumnName("eventdetailcode");
            entity.Property(e => e.Eventoccurrence).HasColumnName("eventoccurrence");
            entity.Property(e => e.Eventsequence).HasColumnName("eventsequence");
            entity.Property(e => e.Eventtime).HasColumnName("eventtime");
            entity.Property(e => e.Eventtimeutc).HasColumnName("eventtimeutc");
            entity.Property(e => e.Eventtype).HasColumnName("eventtype");
            entity.Property(e => e.Exceptiontype).HasColumnName("exceptiontype");
            entity.Property(e => e.Machinename).HasColumnName("machinename");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Requesturl).HasColumnName("requesturl");
        });

        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.HasKey(e => e.ContractorId).HasName("contractors_pkey");

            entity.ToTable("contractors");

            entity.HasIndex(e => e.ContractorId, "contractors_contractor_id_new_idx").IsUnique();

            entity.Property(e => e.ContractorId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasComment("UUID identifier for contractor")
                .HasColumnName("contractor_id");
            entity.Property(e => e.ContractorAddressCiti).HasColumnName("contractor_address_citi");
            entity.Property(e => e.ContractorAddressPostalCode).HasColumnName("contractor_address_postal_code");
            entity.Property(e => e.ContractorAddressStreet).HasColumnName("contractor_address_street");
            entity.Property(e => e.ContractorCountryId).HasColumnName("contractor_country_id");
            entity.Property(e => e.ContractorIsCustomer)
                .HasDefaultValue(false)
                .HasColumnName("contractor_is_customer");
            entity.Property(e => e.ContractorIsSupplier)
                .HasDefaultValue(false)
                .HasColumnName("contractor_is_supplier");
            entity.Property(e => e.ContractorLogo).HasColumnName("contractor_logo");
            entity.Property(e => e.ContractorName).HasColumnName("contractor_name");
            entity.Property(e => e.ContractorPurchasesEmail).HasColumnName("contractor_purchases_email");
            entity.Property(e => e.ContractorPurchasesNotes).HasColumnName("contractor_purchases_notes");
            entity.Property(e => e.ContractorPurchasesPhone).HasColumnName("contractor_purchases_phone");
            entity.Property(e => e.ContractorSalesEmail).HasColumnName("contractor_sales_email");
            entity.Property(e => e.ContractorSalesNotes).HasColumnName("contractor_sales_notes");
            entity.Property(e => e.ContractorSalesPhone).HasColumnName("contractor_sales_phone");
            entity.Property(e => e.ContractorTaxid).HasColumnName("contractor_taxid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.CountryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryCode).HasColumnName("country_code");
            entity.Property(e => e.CountryName).HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("discounts_pkey");

            entity.ToTable("discounts");

            entity.Property(e => e.DiscountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discount_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountName).HasColumnName("discount_name");
            entity.Property(e => e.DiscountType).HasColumnName("discount_type");
            entity.Property(e => e.DiscountValue)
                .HasPrecision(12, 2)
                .HasColumnName("discount_value");
        });

        modelBuilder.Entity<DiscountsProduct>(entity =>
        {
            entity.HasKey(e => e.DiscountProductId).HasName("discounts_products_pkey");

            entity.ToTable("discounts_products");

            entity.Property(e => e.DiscountProductId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discount_product_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountsProducts)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("discounts_products_discount_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.DiscountsProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("discounts_products_product_id_fkey");
        });

        modelBuilder.Entity<DiscountsStore>(entity =>
        {
            entity.HasKey(e => e.DiscountStoreId).HasName("discounts_stores_pkey");

            entity.ToTable("discounts_stores");

            entity.Property(e => e.DiscountStoreId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discount_store_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountsStores)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("discounts_stores_discount_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.DiscountsStores)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("discounts_stores_store_id_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.SupervisorEmployeeId, "idx_employees_supervisor_employee_id");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("employee_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeEmail).HasColumnName("employee_email");
            entity.Property(e => e.EmployeeLogo).HasColumnName("employee_logo");
            entity.Property(e => e.EmployeeName).HasColumnName("employee_name");
            entity.Property(e => e.EmployeePhone).HasColumnName("employee_phone");
            entity.Property(e => e.EmployeePositionId).HasColumnName("employee_position_id");
            entity.Property(e => e.SupervisorEmployeeId).HasColumnName("supervisor_employee_id");

            entity.HasOne(d => d.SupervisorEmployee).WithMany(p => p.InverseSupervisorEmployee)
                .HasForeignKey(d => d.SupervisorEmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_supervisor_employee_id_fkey");
        });

        modelBuilder.Entity<FinancialDocumentsType>(entity =>
        {
            entity.HasKey(e => e.FinancialDocumentTypeId).HasName("financial_documents_types_pkey");

            entity.ToTable("financial_documents_types");

            entity.HasIndex(e => e.TypeCode, "financial_documents_types_type_code_key").IsUnique();

            entity.Property(e => e.FinancialDocumentTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("financial_document_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TypeCode)
                .HasMaxLength(20)
                .HasColumnName("type_code");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("incomes_pkey");

            entity.ToTable("incomes");

            entity.HasIndex(e => e.IncomeCreatedByEmployeeId, "idx_incomes_income_created_by_employee_id");

            entity.HasIndex(e => e.IncomeStatusId, "idx_incomes_income_status_id");

            entity.HasIndex(e => e.PosId, "idx_incomes_pos_id");

            entity.HasIndex(e => e.StoreId, "idx_incomes_store_id");

            entity.HasIndex(e => e.YearId, "idx_incomes_year_id");

            entity.HasIndex(e => e.IncomeNumber, "incomes_income_number_uniq").IsUnique();

            entity.Property(e => e.IncomeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("income_id");
            entity.Property(e => e.ContractorId).HasColumnName("contractor_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IncomeCreatedByEmployeeId).HasColumnName("income_created_by_employee_id");
            entity.Property(e => e.IncomeNumber).HasColumnName("income_number");
            entity.Property(e => e.IncomeStatusId).HasColumnName("income_status_id");
            entity.Property(e => e.PosId).HasColumnName("pos_id");
            entity.Property(e => e.StoreDocumentTypeId)
                .HasComment("Type of income document (PZ, MM+, RI+)")
                .HasColumnName("store_document_type_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TotalCostValue).HasColumnName("total_cost_value");
            entity.Property(e => e.TotalGrossValue).HasColumnName("total_gross_value");
            entity.Property(e => e.TotalNetValue)
                .HasComment("Suma wartości netto wszystkich pozycji - wyliczane jako SUM(incomes_items.net_value)")
                .HasColumnName("total_net_value");
            entity.Property(e => e.YearId).HasColumnName("year_id");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.ContractorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("incomes_contractor_id_fkey");

            entity.HasOne(d => d.IncomeCreatedByEmployee).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IncomeCreatedByEmployeeId)
                .HasConstraintName("incomes_income_created_by_employee_id_fkey");

            entity.HasOne(d => d.IncomeStatus).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IncomeStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incomes_income_status_id_fkey");

            entity.HasOne(d => d.Pos).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.PosId)
                .HasConstraintName("incomes_pos_id_fkey");

            entity.HasOne(d => d.StoreDocumentType).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.StoreDocumentTypeId)
                .HasConstraintName("fk_incomes_store_document_type");

            entity.HasOne(d => d.Store).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("incomes_store_id_fkey");

            entity.HasOne(d => d.Year).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.YearId)
                .HasConstraintName("incomes_year_id_fkey");
        });

        modelBuilder.Entity<IncomesItem>(entity =>
        {
            entity.HasKey(e => e.IncomeItemId).HasName("incomes_items_pkey");

            entity.ToTable("incomes_items");

            entity.HasIndex(e => e.IncomeId, "idx_incomes_items_income_id");

            entity.HasIndex(e => e.ProductId, "idx_incomes_items_product_id");

            entity.HasIndex(e => e.ProductVatRateId, "idx_incomes_items_product_vat_rate_id");

            entity.HasIndex(e => e.IncomeItemId, "incomes_items_income_item_id_key").IsUnique();

            entity.Property(e => e.IncomeItemId)
                .ValueGeneratedNever()
                .HasColumnName("income_item_id");
            entity.Property(e => e.CostPrice)
                .HasPrecision(12, 2)
                .HasComment("Cena kosztowa jednostkowa - cena zakupu/dostawy towaru, szczególnie ważna dla dokumentów MM+")
                .HasColumnName("cost_price");
            entity.Property(e => e.CostValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((cost_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((cost_price * quantity), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)")
                .HasColumnName("cost_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.GrossPrice)
                .HasComputedColumnSql("\nCASE\n    WHEN ((net_price IS NOT NULL) AND (vat_rate_value IS NOT NULL)) THEN round(((net_price * ((100)::numeric + vat_rate_value)) / (100)::numeric), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasColumnName("gross_price");
            entity.Property(e => e.GrossValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((quantity IS NOT NULL) AND (net_price IS NOT NULL) AND (vat_rate_value IS NOT NULL)) THEN round((quantity * round(((net_price * ((100)::numeric + vat_rate_value)) / (100)::numeric), 2)), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasColumnName("gross_value");
            entity.Property(e => e.IncomeId).HasColumnName("income_id");
            entity.Property(e => e.NetPrice).HasColumnName("net_price");
            entity.Property(e => e.NetValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((quantity IS NOT NULL) AND (net_price IS NOT NULL)) THEN round((quantity * net_price), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasColumnName("net_value");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(18, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.VatRateValue).HasColumnName("vat_rate_value");

            entity.HasOne(d => d.Income).WithMany(p => p.IncomesItems)
                .HasForeignKey(d => d.IncomeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incomes_items_income_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.IncomesItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incomes_items_product_id_fkey");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.IncomesItems)
                .HasForeignKey(d => d.ProductVatRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incomes_items_product_vat_rate_id_fkey");
        });

        modelBuilder.Entity<IncomesStatus>(entity =>
        {
            entity.HasKey(e => e.IncomeStatusId).HasName("incomes_statuses_pkey");

            entity.ToTable("incomes_statuses");

            entity.Property(e => e.IncomeStatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("income_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.StatusName).HasColumnName("status_name");
        });

        modelBuilder.Entity<Inorder>(entity =>
        {
            entity.HasKey(e => e.InorderId).HasName("inorders_pkey");

            entity.ToTable("inorders");

            entity.Property(e => e.InorderId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("inorder_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InorderNumber).HasColumnName("inorder_number");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Store).WithMany(p => p.Inorders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("inorders_store_id_fkey");
        });

        modelBuilder.Entity<InordersItem>(entity =>
        {
            entity.HasKey(e => e.InorderItemId).HasName("inorders_items_pkey");

            entity.ToTable("inorders_items");

            entity.Property(e => e.InorderItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("inorder_item_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InorderId).HasColumnName("inorder_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(18, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Inorder).WithMany(p => p.InordersItems)
                .HasForeignKey(d => d.InorderId)
                .HasConstraintName("inorders_items_inorder_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.InordersItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("inorders_items_product_id_fkey");
        });

        modelBuilder.Entity<LoyaltiesWallet>(entity =>
        {
            entity.HasKey(e => e.LoyaltyWalletId).HasName("loyalties_wallets_pkey");

            entity.ToTable("loyalties_wallets");

            entity.Property(e => e.LoyaltyWalletId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("loyalty_wallet_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PointsBalance)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("points_balance");
            entity.Property(e => e.WalletName).HasColumnName("wallet_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => new { e.EmployeeId, e.CreatedAt }, "idx_orders_employee_date");

            entity.HasIndex(e => e.EmployeeId, "idx_orders_employee_id");

            entity.HasIndex(e => e.LoyaltyWalletId, "idx_orders_loyalty_wallet_id");

            entity.HasIndex(e => e.OrderNumber, "idx_orders_order_number").IsUnique();

            entity.HasIndex(e => e.OrderPaymentStatusId, "idx_orders_order_payment_status_id");

            entity.HasIndex(e => e.OrderRealizationTypeId, "idx_orders_order_realization_type_id");

            entity.HasIndex(e => e.PaymentMethodId, "idx_orders_payment_method_id");

            entity.HasIndex(e => e.PosId, "idx_orders_pos_id");

            entity.HasIndex(e => new { e.StoreId, e.CreatedAt }, "idx_orders_store_date");

            entity.HasIndex(e => e.StoreId, "idx_orders_store_id");

            entity.HasIndex(e => new { e.CreatedAt, e.UpdatedAt }, "idx_orders_sync");

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountAmount)
                .HasPrecision(12, 2)
                .HasColumnName("discount_amount");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LoyaltyWalletId).HasColumnName("loyalty_wallet_id");
            entity.Property(e => e.Nip).HasColumnName("nip");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            entity.Property(e => e.OrderPaymentStatusId).HasColumnName("order_payment_status_id");
            entity.Property(e => e.OrderRealizationStatusId).HasColumnName("order_realization_status_id");
            entity.Property(e => e.OrderRealizationTypeId).HasColumnName("order_realization_type_id");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PosId).HasColumnName("pos_id");
            entity.Property(e => e.Source)
                .HasDefaultValueSql("'sales'::text")
                .HasColumnName("source");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.StoreOrderTypeId).HasColumnName("store_order_type_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("orders_employee_id_fkey");

            entity.HasOne(d => d.LoyaltyWallet).WithMany(p => p.Orders)
                .HasForeignKey(d => d.LoyaltyWalletId)
                .HasConstraintName("orders_loyalty_wallet_id_fkey");

            entity.HasOne(d => d.OrderPaymentStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderPaymentStatusId)
                .HasConstraintName("orders_order_payment_status_id_fkey");

            entity.HasOne(d => d.OrderRealizationStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderRealizationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_order_realization_status_id_fkey");

            entity.HasOne(d => d.OrderRealizationType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderRealizationTypeId)
                .HasConstraintName("orders_order_realization_type_id_fkey");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("orders_payment_method_id_fkey");

            entity.HasOne(d => d.Pos).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PosId)
                .HasConstraintName("orders_pos_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("orders_store_id_fkey");

            entity.HasOne(d => d.StoreOrderType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreOrderTypeId)
                .HasConstraintName("orders_store_order_type_id_fkey");
        });

        modelBuilder.Entity<OrdersInvoice>(entity =>
        {
            entity.HasKey(e => e.OrderInvoiceId).HasName("orders_invoices_pkey");

            entity.ToTable("orders_invoices");

            entity.HasIndex(e => e.OrderId, "idx_orders_invoices_order_id");

            entity.Property(e => e.OrderInvoiceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_invoice_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InvoiceNumber).HasColumnName("invoice_number");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderInvoiceTypeId).HasColumnName("order_invoice_type_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersInvoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_invoices_order_id_fkey");

            entity.HasOne(d => d.OrderInvoiceType).WithMany(p => p.OrdersInvoices)
                .HasForeignKey(d => d.OrderInvoiceTypeId)
                .HasConstraintName("orders_invoices_order_invoice_type_id_fkey");
        });

        modelBuilder.Entity<OrdersInvoicesType>(entity =>
        {
            entity.HasKey(e => e.OrderInvoiceTypeId).HasName("orders_invoices_types_pkey");

            entity.ToTable("orders_invoices_types");

            entity.Property(e => e.OrderInvoiceTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_invoice_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InvoiceTypeName).HasColumnName("invoice_type_name");
        });

        modelBuilder.Entity<OrdersItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("orders_items_pkey");

            entity.ToTable("orders_items");

            entity.HasIndex(e => e.DiscountId, "idx_orders_items_discount_id");

            entity.HasIndex(e => e.OrderId, "idx_orders_items_order_id");

            entity.HasIndex(e => e.OrderItemCreatedByEmployeId, "idx_orders_items_order_item_created_by_employe_id");

            entity.HasIndex(e => e.OrderItemProductVatRateId, "idx_orders_items_order_item_product_vat_rate_id");

            entity.HasIndex(e => e.ProductId, "idx_orders_items_product_id");

            entity.HasIndex(e => e.SetItemProductId, "idx_orders_items_set_item_product_id");

            entity.Property(e => e.OrderItemId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("order_item_id");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderItemCourseId)
                .HasComment("ID kursu dla pozycji zamówienia")
                .HasColumnName("order_item_course_id");
            entity.Property(e => e.OrderItemCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("order_item_created_at");
            entity.Property(e => e.OrderItemCreatedByEmployeId)
                .HasComment("ID pracownika tworzącego pozycję")
                .HasColumnName("order_item_created_by_employe_id");
            entity.Property(e => e.OrderItemGrossPrice)
                .HasPrecision(12, 2)
                .HasComment("Cena brutto pozycji")
                .HasColumnName("order_item_gross_price");
            entity.Property(e => e.OrderItemGrossValue)
                .HasPrecision(12, 2)
                .HasComment("Wartość brutto pozycji")
                .HasColumnName("order_item_gross_value");
            entity.Property(e => e.OrderItemIsInSet)
                .HasDefaultValue(false)
                .HasComment("Czy pozycja jest w zestawie")
                .HasColumnName("order_item_is_in_set");
            entity.Property(e => e.OrderItemIsProductDiscounted)
                .HasDefaultValue(false)
                .HasComment("Czy produkt ma rabat")
                .HasColumnName("order_item_is_product_discounted");
            entity.Property(e => e.OrderItemListGrossPrice)
                .HasPrecision(12, 2)
                .HasComment("Cena katalogowa brutto")
                .HasColumnName("order_item_list_gross_price");
            entity.Property(e => e.OrderItemNetValue)
                .HasPrecision(12, 2)
                .HasComment("Wartość netto pozycji")
                .HasColumnName("order_item_net_value");
            entity.Property(e => e.OrderItemOriginQuantity).HasColumnName("order_item_origin_quantity");
            entity.Property(e => e.OrderItemPrice)
                .HasPrecision(12, 2)
                .HasColumnName("order_item_price");
            entity.Property(e => e.OrderItemProductDiscountPercentRatio)
                .HasPrecision(6, 2)
                .HasComment("Procent rabatu produktu")
                .HasColumnName("order_item_product_discount_percent_ratio");
            entity.Property(e => e.OrderItemProductVatRateId)
                .HasComment("ID stawki VAT produktu - wymagane, nie może być NULL")
                .HasColumnName("order_item_product_vat_rate_id");
            entity.Property(e => e.OrderItemQuantity)
                .HasPrecision(12, 3)
                .HasColumnName("order_item_quantity");
            entity.Property(e => e.OrderItemSetItemDiscountPercentRatio)
                .HasPrecision(6, 2)
                .HasComment("Procent rabatu pozycji zestawu")
                .HasColumnName("order_item_set_item_discount_percent_ratio");
            entity.Property(e => e.OrderItemTotal)
                .HasPrecision(12, 2)
                .HasColumnName("order_item_total");
            entity.Property(e => e.OrderItemVatRateRatio)
                .HasPrecision(6, 2)
                .HasComment("Stawka VAT pozycji")
                .HasColumnName("order_item_vat_rate_ratio");
            entity.Property(e => e.OrderItemVatValue)
                .HasPrecision(12, 2)
                .HasComment("Wartość VAT pozycji")
                .HasColumnName("order_item_vat_value");
            entity.Property(e => e.ProductDiscountId)
                .HasComment("ID rabatu produktu")
                .HasColumnName("product_discount_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SetItemProductId).HasColumnName("set_item_product_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Discount).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("orders_items_discount_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_items_order_id_fkey");

            entity.HasOne(d => d.OrderItemCreatedByEmploye).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.OrderItemCreatedByEmployeId)
                .HasConstraintName("orders_items_order_item_created_by_employe_id_fkey");

            entity.HasOne(d => d.OrderItemProductVatRate).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.OrderItemProductVatRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_items_order_item_product_vat_rate_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("orders_items_product_id_fkey");

            entity.HasOne(d => d.SetItemProduct).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.SetItemProductId)
                .HasConstraintName("orders_items_set_item_product_id_fkey");
        });

        modelBuilder.Entity<OrdersItemsOperationsLog>(entity =>
        {
            entity.HasKey(e => e.OrderItemOperationLogId).HasName("orders_items_operations_log_pkey");

            entity.ToTable("orders_items_operations_log");

            entity.Property(e => e.OrderItemOperationLogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_item_operation_log_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.NewOrderItemStatusId).HasColumnName("new_order_item_status_id");
            entity.Property(e => e.OldOrderItemStatusId).HasColumnName("old_order_item_status_id");
            entity.Property(e => e.OperationDescription).HasColumnName("operation_description");
            entity.Property(e => e.OperationType).HasColumnName("operation_type");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.OrderItemOperationEmployeeId).HasColumnName("order_item_operation_employee_id");

            entity.HasOne(d => d.OrderItemOperationEmployee).WithMany(p => p.OrdersItemsOperationsLogs)
                .HasForeignKey(d => d.OrderItemOperationEmployeeId)
                .HasConstraintName("orders_items_operations_log_order_item_operation_employee_id_fk");
        });

        modelBuilder.Entity<OrdersOperationsLog>(entity =>
        {
            entity.HasKey(e => e.OrderOperationLogId).HasName("orders_operations_log_pkey");

            entity.ToTable("orders_operations_log");

            entity.HasIndex(e => e.OrderId, "idx_orders_operations_log_order_id");

            entity.Property(e => e.OrderOperationLogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_operation_log_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OperationDescription).HasColumnName("operation_description");
            entity.Property(e => e.OperationType).HasColumnName("operation_type");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderOperationEmployeeId).HasColumnName("order_operation_employee_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersOperationsLogs)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_operations_log_order_id_fkey");

            entity.HasOne(d => d.OrderOperationEmployee).WithMany(p => p.OrdersOperationsLogs)
                .HasForeignKey(d => d.OrderOperationEmployeeId)
                .HasConstraintName("orders_operations_log_order_operation_employee_id_fkey");
        });

        modelBuilder.Entity<OrdersPaymentsStatus>(entity =>
        {
            entity.HasKey(e => e.OrderPaymentStatusId).HasName("orders_payments_statuses_pkey");

            entity.ToTable("orders_payments_statuses");

            entity.HasIndex(e => e.OrderPaymentStatusId, "orders_payments_statuses_order_payment_status_id_key").IsUnique();

            entity.Property(e => e.OrderPaymentStatusId)
                .ValueGeneratedNever()
                .HasColumnName("order_payment_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderPaymentStatusLogo).HasColumnName("order_payment_status_logo");
            entity.Property(e => e.OrderPaymentStatusName).HasColumnName("order_payment_status_name");
        });

        modelBuilder.Entity<OrdersRealizationsStatus>(entity =>
        {
            entity.HasKey(e => e.OrderRealizationStatusId).HasName("orders_realizations_statuses_pkey");

            entity.ToTable("orders_realizations_statuses");

            entity.HasIndex(e => e.OrderRealizationStatusId, "orders_realizations_statuses_order_realization_status_id_key").IsUnique();

            entity.Property(e => e.OrderRealizationStatusId)
                .ValueGeneratedNever()
                .HasColumnName("order_realization_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderRealizationStatusLogo).HasColumnName("order_realization_status_logo");
            entity.Property(e => e.OrderRealizationStatusName).HasColumnName("order_realization_status_name");
        });

        modelBuilder.Entity<OrdersRealizationsType>(entity =>
        {
            entity.HasKey(e => e.OrderRealizationTypeId).HasName("orders_realizations_types_pkey");

            entity.ToTable("orders_realizations_types");

            entity.Property(e => e.OrderRealizationTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_realization_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.RealizationTypeName).HasColumnName("realization_type_name");
        });

        modelBuilder.Entity<OrdersRealizationsTypesStatusesPath>(entity =>
        {
            entity.HasKey(e => e.OrderRealizationTypeStatusPathId).HasName("orders_realizations_types_statuses_paths_pkey");

            entity.ToTable("orders_realizations_types_statuses_paths");

            entity.Property(e => e.OrderRealizationTypeStatusPathId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_realization_type_status_path_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderRealizationTypeId).HasColumnName("order_realization_type_id");
            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

            entity.HasOne(d => d.OrderRealizationType).WithMany(p => p.OrdersRealizationsTypesStatusesPaths)
                .HasForeignKey(d => d.OrderRealizationTypeId)
                .HasConstraintName("orders_realizations_types_status_order_realization_type_id_fkey");
        });

        modelBuilder.Entity<OrdersVatSummary>(entity =>
        {
            entity.HasKey(e => e.OrderVatSummaryId).HasName("orders_vat_summaries_pkey");

            entity.ToTable("orders_vat_summaries");

            entity.HasIndex(e => e.OrderId, "idx_orders_vat_summaries_order_id");

            entity.Property(e => e.OrderVatSummaryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_vat_summary_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.VatAmount)
                .HasPrecision(12, 2)
                .HasColumnName("vat_amount");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersVatSummaries)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_vat_summaries_order_id_fkey");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.OrdersVatSummaries)
                .HasForeignKey(d => d.ProductVatRateId)
                .HasConstraintName("orders_vat_summaries_product_vat_rate_id_fkey");
        });

        modelBuilder.Entity<Outcome>(entity =>
        {
            entity.HasKey(e => e.OutcomeId).HasName("outcomes_pkey");

            entity.ToTable("outcomes");

            entity.HasIndex(e => e.ContractorId, "idx_outcomes_contractor_id");

            entity.HasIndex(e => e.OrderId, "idx_outcomes_order_id");

            entity.HasIndex(e => e.OutcomeStatusId, "idx_outcomes_outcome_status_id");

            entity.HasIndex(e => e.PosId, "idx_outcomes_pos_id");

            entity.HasIndex(e => e.StoreId, "idx_outcomes_store_id");

            entity.HasIndex(e => e.CreatedAt, "idx_outcomes_sync");

            entity.HasIndex(e => e.YearId, "idx_outcomes_year_id");

            entity.HasIndex(e => e.OutcomeNumber, "outcomes_outcome_number_uniq").IsUnique();

            entity.Property(e => e.OutcomeId)
                .ValueGeneratedNever()
                .HasColumnName("outcome_id");
            entity.Property(e => e.ContractorId).HasColumnName("contractor_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OutcomeCreatedByEmployeeId).HasColumnName("outcome_created_by_employee_id");
            entity.Property(e => e.OutcomeNumber).HasColumnName("outcome_number");
            entity.Property(e => e.OutcomeStatusId).HasColumnName("outcome_status_id");
            entity.Property(e => e.PosId).HasColumnName("pos_id");
            entity.Property(e => e.StoreDocumentTypeId).HasColumnName("store_document_type_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TotalCostValue).HasColumnName("total_cost_value");
            entity.Property(e => e.TotalGrossValue)
                .HasComment("Suma wartości brutto wszystkich pozycji - wyliczane jako SUM(outcomes_items.gross_value)")
                .HasColumnName("total_gross_value");
            entity.Property(e => e.TotalNetValue)
                .HasComment("Suma wartości netto wszystkich pozycji - wyliczane jako SUM(outcomes_items.net_value)")
                .HasColumnName("total_net_value");
            entity.Property(e => e.YearId).HasColumnName("year_id");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.ContractorId)
                .HasConstraintName("outcomes_contractor_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("outcomes_order_id_fkey");

            entity.HasOne(d => d.OutcomeCreatedByEmployee).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.OutcomeCreatedByEmployeeId)
                .HasConstraintName("outcomes_outcome_created_by_employee_id_fkey");

            entity.HasOne(d => d.OutcomeStatus).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.OutcomeStatusId)
                .HasConstraintName("outcomes_outcome_status_id_fkey");

            entity.HasOne(d => d.Pos).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.PosId)
                .HasConstraintName("outcomes_pos_id_fkey");

            entity.HasOne(d => d.StoreDocumentType).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.StoreDocumentTypeId)
                .HasConstraintName("fk_outcomes_store_document_type");

            entity.HasOne(d => d.Store).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("outcomes_store_id_fkey");

            entity.HasOne(d => d.Year).WithMany(p => p.Outcomes)
                .HasForeignKey(d => d.YearId)
                .HasConstraintName("outcomes_year_id_fkey");
        });

        modelBuilder.Entity<OutcomesFinancialDocument>(entity =>
        {
            entity.HasKey(e => e.OutcomeFinancialDocumentId).HasName("outcomes_financial_documents_pkey");

            entity.ToTable("outcomes_financial_documents", tb => tb.HasComment("Dokumenty finansowe sprzedaży (paragony, faktury)"));

            entity.HasIndex(e => e.OrderId, "idx_outcomes_financial_documents_order_id");

            entity.HasIndex(e => e.OutcomeId, "idx_outcomes_financial_documents_outcome_id");

            entity.HasIndex(e => e.OrderId, "idx_outcomes_receipts_order_id");

            entity.Property(e => e.OutcomeFinancialDocumentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("outcome_financial_document_id");
            entity.Property(e => e.ContractorId)
                .HasComment("ID kontrahenta/klienta")
                .HasColumnName("contractor_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerName)
                .HasComment("Nazwa klienta")
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerNip)
                .HasMaxLength(20)
                .HasComment("NIP klienta")
                .HasColumnName("customer_nip");
            entity.Property(e => e.FinancialDocumentDate)
                .HasComment("Data dokumentu finansowego")
                .HasColumnName("financial_document_date");
            entity.Property(e => e.FinancialDocumentNumber)
                .HasMaxLength(20)
                .HasComment("Numer dokumentu finansowego")
                .HasColumnName("financial_document_number");
            entity.Property(e => e.FinancialDocumentStatusId)
                .HasComment("Status dokumentu finansowego")
                .HasColumnName("financial_document_status_id");
            entity.Property(e => e.FinancialDocumentTypeId)
                .HasComment("Typ dokumentu finansowego")
                .HasColumnName("financial_document_type_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OutcomeId).HasColumnName("outcome_id");
            entity.Property(e => e.PosId).HasColumnName("pos_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TotalCostValue)
                .HasComment("Suma wartości kosztowych wszystkich pozycji")
                .HasColumnName("total_cost_value");
            entity.Property(e => e.TotalGrossValue)
                .HasComment("Suma wartości brutto wszystkich pozycji")
                .HasColumnName("total_gross_value");
            entity.Property(e => e.TotalNetValue)
                .HasComment("Suma wartości netto wszystkich pozycji")
                .HasColumnName("total_net_value");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearId).HasColumnName("year_id");

            entity.HasOne(d => d.Contractor).WithMany(p => p.OutcomesFinancialDocuments)
                .HasForeignKey(d => d.ContractorId)
                .HasConstraintName("fk_outcomes_financial_documents_contractor_id");

            entity.HasOne(d => d.FinancialDocumentStatus).WithMany(p => p.OutcomesFinancialDocuments)
                .HasForeignKey(d => d.FinancialDocumentStatusId)
                .HasConstraintName("fk_outcomes_financial_documents_status_id");

            entity.HasOne(d => d.FinancialDocumentType).WithMany(p => p.OutcomesFinancialDocuments)
                .HasForeignKey(d => d.FinancialDocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_outcomes_financial_documents_type_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OutcomesFinancialDocuments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("outcomes_receipts_order_id_fkey");

            entity.HasOne(d => d.Outcome).WithMany(p => p.OutcomesFinancialDocuments)
                .HasForeignKey(d => d.OutcomeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_outcomes_financial_documents_outcome_id");
        });

        modelBuilder.Entity<OutcomesFinancialDocumentsItem>(entity =>
        {
            entity.HasKey(e => e.OutcomeFinancialDocumentItemId).HasName("outcomes_financial_documents_items_pkey");

            entity.ToTable("outcomes_financial_documents_items", tb => tb.HasComment("Pozycje dokumentów finansowych sprzedaży"));

            entity.HasIndex(e => e.OutcomeFinancialDocumentId, "idx_outcomes_financial_documents_items_document_id");

            entity.HasIndex(e => e.OutcomeItemId, "idx_outcomes_financial_documents_items_outcome_item_id");

            entity.Property(e => e.OutcomeFinancialDocumentItemId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("outcome_financial_document_item_id");
            entity.Property(e => e.CostPrice)
                .HasPrecision(18, 2)
                .HasComment("Cena kosztowa netto jednostkowa - dla celów raportowych (marża)")
                .HasColumnName("cost_price");
            entity.Property(e => e.CostValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((cost_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((cost_price * quantity), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)")
                .HasColumnName("cost_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedByEmployeeId).HasColumnName("created_by_employee_id");
            entity.Property(e => e.GrossPrice)
                .HasPrecision(18, 2)
                .HasColumnName("gross_price");
            entity.Property(e => e.GrossValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((gross_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((quantity * gross_price), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość brutto pozycji - automatycznie obliczana jako ROUND(quantity * gross_price, 2)")
                .HasColumnName("gross_value");
            entity.Property(e => e.NetValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((gross_price IS NOT NULL) AND (quantity IS NOT NULL) AND (vat_rate_value IS NOT NULL)) THEN round(((quantity * gross_price) / ((1)::numeric + (vat_rate_value / (100)::numeric))), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość netto pozycji - automatycznie obliczana jako ROUND((quantity * gross_price) / (1 + vat_rate_value/100), 2)")
                .HasColumnName("net_value");
            entity.Property(e => e.OutcomeFinancialDocumentId).HasColumnName("outcome_financial_document_id");
            entity.Property(e => e.OutcomeItemId).HasColumnName("outcome_item_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(18, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.VatRateValue)
                .HasPrecision(5, 4)
                .HasColumnName("vat_rate_value");
            entity.Property(e => e.VatValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((gross_price IS NOT NULL) AND (quantity IS NOT NULL) AND (vat_rate_value IS NOT NULL)) THEN round(((quantity * gross_price) - ((quantity * gross_price) / ((1)::numeric + (vat_rate_value / (100)::numeric)))), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość VAT pozycji - automatycznie wyliczana z gross_price, quantity i przechowywanego vat_rate_value")
                .HasColumnName("vat_value");

            entity.HasOne(d => d.CreatedByEmployee).WithMany(p => p.OutcomesFinancialDocumentsItems)
                .HasForeignKey(d => d.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("outcomes_financial_documents_items_created_by_employee_id_fkey");

            entity.HasOne(d => d.OutcomeFinancialDocument).WithMany(p => p.OutcomesFinancialDocumentsItems)
                .HasForeignKey(d => d.OutcomeFinancialDocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_outcomes_financial_documents_items_document_id");

            entity.HasOne(d => d.Product).WithMany(p => p.OutcomesFinancialDocumentsItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_outcomes_financial_documents_items_product_id");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.OutcomesFinancialDocumentsItems)
                .HasForeignKey(d => d.ProductVatRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_outcomes_financial_documents_items_vat_rate_id");
        });

        modelBuilder.Entity<OutcomesFinancialDocumentsStatus>(entity =>
        {
            entity.HasKey(e => e.OutcomeFinancialDocumentStatusId).HasName("outcomes_financial_documents_statuses_pkey");

            entity.ToTable("outcomes_financial_documents_statuses");

            entity.Property(e => e.OutcomeFinancialDocumentStatusId).HasColumnName("outcome_financial_document_status_id");
            entity.Property(e => e.OutcomeFinancialDocumentStatusName)
                .HasColumnType("character varying")
                .HasColumnName("outcome_financial_document_status_name");
        });

        modelBuilder.Entity<OutcomesFinancialDocumentsVatSummary>(entity =>
        {
            entity.HasKey(e => e.OutcomeFinancialDocumentVatSummaryId).HasName("outcomes_financial_documents_vat_summaries_pkey");

            entity.ToTable("outcomes_financial_documents_vat_summaries", tb => tb.HasComment("Podsumowania VAT dla dokumentów finansowych sprzedaży"));

            entity.Property(e => e.OutcomeFinancialDocumentVatSummaryId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("outcome_financial_document_vat_summary_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FinancialDocumentSummaryGrossValue)
                .HasPrecision(18, 2)
                .HasColumnName("financial_document_summary_gross_value");
            entity.Property(e => e.FinancialDocumentSummaryNetValue)
                .HasPrecision(18, 2)
                .HasColumnName("financial_document_summary_net_value");
            entity.Property(e => e.FinancialDocumentSummaryVatValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((financial_document_summary_gross_value IS NOT NULL) AND (financial_document_summary_net_value IS NOT NULL)) THEN round((financial_document_summary_gross_value - financial_document_summary_net_value), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość VAT - automatycznie wyliczana jako gross_value - net_value na podstawie przechowywanego vat_rate_value")
                .HasColumnName("financial_document_summary_vat_value");
            entity.Property(e => e.OutcomeFinancialDocumentId).HasColumnName("outcome_financial_document_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.VatRateValue)
                .HasComment("Wartość stawki VAT (wstawiana z products_vat_rates przy tworzeniu rekordu) - przechowywana historycznie")
                .HasColumnName("vat_rate_value");

            entity.HasOne(d => d.OutcomeFinancialDocument).WithMany(p => p.OutcomesFinancialDocumentsVatSummaries)
                .HasForeignKey(d => d.OutcomeFinancialDocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_outcomes_financial_documents_vat_summaries_document_id");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.OutcomesFinancialDocumentsVatSummaries)
                .HasForeignKey(d => d.ProductVatRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_outcomes_financial_documents_vat_summaries_vat_rate_id");
        });

        modelBuilder.Entity<OutcomesItem>(entity =>
        {
            entity.HasKey(e => e.OutcomeItemId).HasName("outcomes_items_pkey");

            entity.ToTable("outcomes_items");

            entity.HasIndex(e => e.OutcomeId, "idx_outcomes_items_outcome_id");

            entity.HasIndex(e => e.ProductId, "idx_outcomes_items_product_id");

            entity.Property(e => e.OutcomeItemId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("outcome_item_id");
            entity.Property(e => e.CostPrice)
                .HasPrecision(12, 2)
                .HasComment("Cena kosztowa jednostkowa - cena zakupu/dostawy towaru, szczególnie ważna dla dokumentów MM+")
                .HasColumnName("cost_price");
            entity.Property(e => e.CostValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((cost_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((cost_price * quantity), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)")
                .HasColumnName("cost_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.GrossPrice)
                .HasComment("Cena brutto jednostkowa - cena sprzedaży brutto")
                .HasColumnName("gross_price");
            entity.Property(e => e.GrossValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((gross_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((quantity * gross_price), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość brutto pozycji - automatycznie obliczana jako ROUND(quantity * gross_price, 2)")
                .HasColumnName("gross_value");
            entity.Property(e => e.NetPrice)
                .HasComment("Cena netto sprzedaży jednostkowa - używana przy sprzedaży")
                .HasColumnName("net_price");
            entity.Property(e => e.NetValue)
                .HasComputedColumnSql("\nCASE\n    WHEN ((net_price IS NOT NULL) AND (quantity IS NOT NULL)) THEN round((quantity * net_price), 2)\n    ELSE NULL::numeric\nEND", true)
                .HasComment("Wartość netto pozycji - automatycznie obliczana jako ROUND(quantity * net_price, 2)")
                .HasColumnName("net_value");
            entity.Property(e => e.OutcomeId).HasColumnName("outcome_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(18, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Outcome).WithMany(p => p.OutcomesItems)
                .HasForeignKey(d => d.OutcomeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_outcomes_items_outcome_id");

            entity.HasOne(d => d.Product).WithMany(p => p.OutcomesItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("outcomes_items_product_id_fkey");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.OutcomesItems)
                .HasForeignKey(d => d.ProductVatRateId)
                .HasConstraintName("fk_outcomes_items_product_vat_rate_id");
        });

        modelBuilder.Entity<OutcomesStatus>(entity =>
        {
            entity.HasKey(e => e.OutcomeStatusId).HasName("outcomes_statuses_pkey");

            entity.ToTable("outcomes_statuses");

            entity.Property(e => e.OutcomeStatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("outcome_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.StatusName).HasColumnName("status_name");
        });

        modelBuilder.Entity<PaymentsMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("payments_methods_pkey");

            entity.ToTable("payments_methods");

            entity.Property(e => e.PaymentMethodId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("payment_method_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PaymentMethodName).HasColumnName("payment_method_name");
        });

        modelBuilder.Entity<Pose>(entity =>
        {
            entity.HasKey(e => e.PosId).HasName("poses_pkey");

            entity.ToTable("poses");

            entity.HasIndex(e => e.PosTypeId, "idx_poses_pos_type_id");

            entity.HasIndex(e => e.StoreId, "idx_poses_store_id");

            entity.Property(e => e.PosId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("pos_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PosName).HasColumnName("pos_name");
            entity.Property(e => e.PosTypeId).HasColumnName("pos_type_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.PosType).WithMany(p => p.Poses)
                .HasForeignKey(d => d.PosTypeId)
                .HasConstraintName("poses_pos_type_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Poses)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("poses_store_id_fkey");
        });

        modelBuilder.Entity<PosesType>(entity =>
        {
            entity.HasKey(e => e.PosTypeId).HasName("poses_types_pkey");

            entity.ToTable("poses_types");

            entity.Property(e => e.PosTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("pos_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PosTypeName).HasColumnName("pos_type_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => new { e.ProductCategory1Id, e.IsVisible }, "idx_products_category_visible");

            entity.HasIndex(e => e.ProductCategory1Id, "idx_products_product_category1_id");

            entity.HasIndex(e => e.ProductCategory2Id, "idx_products_product_category2_id");

            entity.HasIndex(e => e.ProductQuantityUnitId, "idx_products_product_quantity_unit_id");

            entity.HasIndex(e => e.ProductVatRateId, "idx_products_product_vat_rate_id");

            entity.HasIndex(e => new { e.IsSellable, e.IsVisible }, "idx_products_sellable_visible");

            entity.HasIndex(e => new { e.CreatedAt, e.UpdatedAt }, "idx_products_sync");

            entity.Property(e => e.ProductId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountedPrice).HasColumnName("discounted_price");
            entity.Property(e => e.IsComposite)
                .HasDefaultValue(false)
                .HasColumnName("is_composite");
            entity.Property(e => e.IsSellable)
                .HasDefaultValue(true)
                .HasColumnName("is_sellable");
            entity.Property(e => e.IsVisible)
                .HasDefaultValue(true)
                .HasColumnName("is_visible");
            entity.Property(e => e.MinStockLevel).HasColumnName("min_stock_level");
            entity.Property(e => e.OriginalPrice).HasColumnName("original_price");
            entity.Property(e => e.ProductCategory1Id).HasColumnName("product_category1_id");
            entity.Property(e => e.ProductCategory2Id).HasColumnName("product_category2_id");
            entity.Property(e => e.ProductDescription).HasColumnName("product_description");
            entity.Property(e => e.ProductName).HasColumnName("product_name");
            entity.Property(e => e.ProductPrice)
                .HasPrecision(12, 2)
                .HasColumnName("product_price");
            entity.Property(e => e.ProductQuantityUnitId).HasColumnName("product_quantity_unit_id");
            entity.Property(e => e.ProductVatRateId).HasColumnName("product_vat_rate_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ProductCategory1).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory1Id)
                .HasConstraintName("products_product_category1_id_fkey");

            entity.HasOne(d => d.ProductCategory2).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory2Id)
                .HasConstraintName("products_product_category2_id_fkey");

            entity.HasOne(d => d.ProductQuantityUnit).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductQuantityUnitId)
                .HasConstraintName("products_product_quantity_unit_id_fkey");

            entity.HasOne(d => d.ProductVatRate).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductVatRateId)
                .HasConstraintName("products_product_vat_rate_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_products_supplier_id");
        });

        modelBuilder.Entity<ProductSetsItem>(entity =>
        {
            entity.HasKey(e => e.ProductSetId).HasName("product_sets_items_pkey");

            entity.ToTable("product_sets_items");

            entity.Property(e => e.ProductSetId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_set_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SetName).HasColumnName("set_name");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSetsItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_sets_items_product_id_fkey");
        });

        modelBuilder.Entity<ProductsBarcode>(entity =>
        {
            entity.HasKey(e => e.ProductBarcodeId).HasName("products_barcodes_pkey");

            entity.ToTable("products_barcodes");

            entity.HasIndex(e => e.ProductId, "idx_products_barcodes_product_id");

            entity.HasIndex(e => e.Barcode, "products_barcodes_barcode_key").IsUnique();

            entity.Property(e => e.ProductBarcodeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_barcode_id");
            entity.Property(e => e.Barcode).HasColumnName("barcode");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsBarcodes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("products_barcodes_product_id_fkey");
        });

        modelBuilder.Entity<ProductsCategories1>(entity =>
        {
            entity.HasKey(e => e.ProductCategory1Id).HasName("products_categories1_pkey");

            entity.ToTable("products_categories1");

            entity.Property(e => e.ProductCategory1Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_category1_id");
            entity.Property(e => e.Category1Name).HasColumnName("category1_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<ProductsCategories2>(entity =>
        {
            entity.HasKey(e => e.ProductCategory2Id).HasName("products_categories2_pkey");

            entity.ToTable("products_categories2");

            entity.Property(e => e.ProductCategory2Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_category2_id");
            entity.Property(e => e.Category2Name).HasColumnName("category2_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductCategory1Id).HasColumnName("product_category1_id");

            entity.HasOne(d => d.ProductCategory1).WithMany(p => p.ProductsCategories2s)
                .HasForeignKey(d => d.ProductCategory1Id)
                .HasConstraintName("products_categories2_product_category1_id_fkey");
        });

        modelBuilder.Entity<ProductsQuantityUnit>(entity =>
        {
            entity.HasKey(e => e.ProductQuantityUnitId).HasName("products_quantity_units_pkey");

            entity.ToTable("products_quantity_units");

            entity.Property(e => e.ProductQuantityUnitId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_quantity_unit_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UnitName).HasColumnName("unit_name");
            entity.Property(e => e.UnitSymbol).HasColumnName("unit_symbol");
        });

        modelBuilder.Entity<ProductsRecipe>(entity =>
        {
            entity.HasKey(e => e.ProductRecipeId).HasName("products_recipes_pkey");

            entity.ToTable("products_recipes");

            entity.Property(e => e.ProductRecipeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_recipe_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RecipeName).HasColumnName("recipe_name");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsRecipes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("products_recipes_product_id_fkey");
        });

        modelBuilder.Entity<ProductsRecipesItem>(entity =>
        {
            entity.HasKey(e => e.ProductRecipeItemId).HasName("products_recipes_items_pkey");

            entity.ToTable("products_recipes_items");

            entity.Property(e => e.ProductRecipeItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_recipe_item_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductRecipeId).HasColumnName("product_recipe_id");
            entity.Property(e => e.ProductRecipeItemProductId).HasColumnName("product_recipe_item_product_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsRecipesItemProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_recipes_items_product_id_fkey");

            entity.HasOne(d => d.ProductRecipe).WithMany(p => p.ProductsRecipesItems)
                .HasForeignKey(d => d.ProductRecipeId)
                .HasConstraintName("products_recipes_items_product_recipe_id_fkey");

            entity.HasOne(d => d.ProductRecipeItemProduct).WithMany(p => p.ProductsRecipesItemProductRecipeItemProducts)
                .HasForeignKey(d => d.ProductRecipeItemProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_recipes_items_product_recipe_item_product_id_fkey");
        });

        modelBuilder.Entity<ProductsVatRate>(entity =>
        {
            entity.HasKey(e => e.ProductVatRateId).HasName("products_vat_rates_pkey");

            entity.ToTable("products_vat_rates");

            entity.Property(e => e.ProductVatRateId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_vat_rate_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.VatRateName).HasColumnName("vat_rate_name");
            entity.Property(e => e.VatRateValue)
                .HasPrecision(6, 2)
                .HasColumnName("vat_rate_value");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.SetId).HasName("sets_pkey");

            entity.ToTable("sets");

            entity.HasIndex(e => e.SetCategorieId, "idx_sets_set_categorie_id");

            entity.Property(e => e.SetId).HasColumnName("set_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.SetActiveFrom).HasColumnName("set_active_from");
            entity.Property(e => e.SetActiveTo).HasColumnName("set_active_to");
            entity.Property(e => e.SetCategorieId).HasColumnName("set_categorie_id");
            entity.Property(e => e.SetIsDisplayed)
                .HasDefaultValue(true)
                .HasColumnName("set_is_displayed");
            entity.Property(e => e.SetName).HasColumnName("set_name");

            entity.HasOne(d => d.SetCategorie).WithMany(p => p.Sets)
                .HasForeignKey(d => d.SetCategorieId)
                .HasConstraintName("sets_set_categorie_id_fkey");
        });

        modelBuilder.Entity<SetsCategory>(entity =>
        {
            entity.HasKey(e => e.SetCategorieId).HasName("sets_categories_pkey");

            entity.ToTable("sets_categories");

            entity.Property(e => e.SetCategorieId).HasColumnName("set_categorie_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.SetCategorieName).HasColumnName("set_categorie_name");
        });

        modelBuilder.Entity<SetsItem>(entity =>
        {
            entity.HasKey(e => e.SetItemId).HasName("sets_items_pkey");

            entity.ToTable("sets_items");

            entity.HasIndex(e => e.SetId, "idx_sets_items_set_id");

            entity.Property(e => e.SetItemId).HasColumnName("set_item_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.SetId).HasColumnName("set_id");
            entity.Property(e => e.SetItemDiscountPercentRatio)
                .HasDefaultValueSql("0")
                .HasColumnName("set_item_discount_percent_ratio");
            entity.Property(e => e.SetItemName).HasColumnName("set_item_name");
            entity.Property(e => e.SetItemPrice)
                .HasDefaultValueSql("0")
                .HasColumnName("set_item_price");

            entity.HasOne(d => d.Set).WithMany(p => p.SetsItems)
                .HasForeignKey(d => d.SetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sets_items_set_id_fkey");
        });

        modelBuilder.Entity<SetsItemsProduct>(entity =>
        {
            entity.HasKey(e => e.SetItemProductId).HasName("sets_items_products_pkey");

            entity.ToTable("sets_items_products");

            entity.HasIndex(e => e.ProductId, "idx_sets_items_products_product_id");

            entity.HasIndex(e => e.SetItemId, "idx_sets_items_products_set_item_id");

            entity.Property(e => e.SetItemProductId).HasColumnName("set_item_product_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SetItemId).HasColumnName("set_item_id");
            entity.Property(e => e.SetItemProductIsActive)
                .HasDefaultValue(true)
                .HasColumnName("set_item_product_is_active");

            entity.HasOne(d => d.Product).WithMany(p => p.SetsItemsProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sets_items_products_product_id_fkey");

            entity.HasOne(d => d.SetItem).WithMany(p => p.SetsItemsProducts)
                .HasForeignKey(d => d.SetItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sets_items_products_set_item_id_fkey");
        });

        modelBuilder.Entity<SetsSchedule>(entity =>
        {
            entity.HasKey(e => e.SetScheduleId).HasName("sets_schedules_pkey");

            entity.ToTable("sets_schedules");

            entity.HasIndex(e => e.SetId, "idx_sets_schedules_set_id");

            entity.Property(e => e.SetScheduleId).HasColumnName("set_schedule_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.SetId).HasColumnName("set_id");
            entity.Property(e => e.SetScheduleDaysOfWeek).HasColumnName("set_schedule_days_of_week");
            entity.Property(e => e.SetScheduleHoursFrom).HasColumnName("set_schedule_hours_from");
            entity.Property(e => e.SetScheduleHoursTo).HasColumnName("set_schedule_hours_to");

            entity.HasOne(d => d.Set).WithMany(p => p.SetsSchedules)
                .HasForeignKey(d => d.SetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sets_schedules_set_id_fkey");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shops_pkey");

            entity.ToTable("shops");

            entity.HasIndex(e => e.ManagerId, "idx_shops_manager_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.ShopName).HasColumnName("shop_name");

            entity.HasOne(d => d.Manager).WithMany(p => p.Shops)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("shops_manager_id_fkey");
        });

        modelBuilder.Entity<ShopsSet>(entity =>
        {
            entity.HasKey(e => e.ShopSetId).HasName("shops_sets_pkey");

            entity.ToTable("shops_sets");

            entity.HasIndex(e => e.SetId, "idx_shops_sets_set_id");

            entity.HasIndex(e => e.ShopId, "idx_shops_sets_shop_id");

            entity.Property(e => e.ShopSetId).HasColumnName("shop_set_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.SetId).HasColumnName("set_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Set).WithMany(p => p.ShopsSets)
                .HasForeignKey(d => d.SetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shops_sets_set_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopsSets)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shops_sets_shop_id_fkey");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("stocks_pkey");

            entity.ToTable("stocks");

            entity.HasIndex(e => e.ProductId, "idx_stocks_product_id");

            entity.HasIndex(e => new { e.ProductId, e.StoreId }, "idx_stocks_product_store");

            entity.HasIndex(e => e.StoreId, "idx_stocks_store_id");

            entity.HasIndex(e => e.YearId, "idx_stocks_year_id");

            entity.HasIndex(e => e.StockId, "stocks_stock_id_key").IsUnique();

            entity.HasIndex(e => new { e.ProductId, e.StoreId, e.YearId }, "uq_stocks_product_store_year").IsUnique();

            entity.Property(e => e.StockId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("stock_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.StockAvailableQuantity)
                .HasComputedColumnSql("(((stock_incomes_quantity - stock_outcomes_quantity) - stock_blocked_quantity) - stock_orders_quantity)", true)
                .HasColumnName("stock_available_quantity");
            entity.Property(e => e.StockBlockedQuantity)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_blocked_quantity");
            entity.Property(e => e.StockIncomesNotYetAllowedQuantity)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_incomes_not_yet_allowed_quantity");
            entity.Property(e => e.StockIncomesQuantity)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_incomes_quantity");
            entity.Property(e => e.StockMinQuantity).HasColumnName("stock_min_quantity");
            entity.Property(e => e.StockOrdersQuantity)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_orders_quantity");
            entity.Property(e => e.StockOutcomesQuantity)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_outcomes_quantity");
            entity.Property(e => e.StockTotalQuantity)
                .HasPrecision(18, 3)
                .HasComputedColumnSql("(stock_incomes_quantity - stock_outcomes_quantity)", true)
                .HasColumnName("stock_total_quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearId).HasColumnName("year_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stocks_product_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stocks_store_id_fkey");

            entity.HasOne(d => d.Year).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.YearId)
                .HasConstraintName("stocks_year_id_fkey");
        });

        modelBuilder.Entity<StockHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stock_history_pkey");

            entity.ToTable("stock_history");

            entity.HasIndex(e => e.EmployeeId, "idx_stock_history_employee_id");

            entity.HasIndex(e => new { e.ProductId, e.CreatedAt }, "idx_stock_history_product_date");

            entity.HasIndex(e => e.ProductId, "idx_stock_history_product_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DocumentNumber).HasColumnName("document_number");
            entity.Property(e => e.DocumentType).HasColumnName("document_type");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.QuantityAfter)
                .HasPrecision(18, 3)
                .HasColumnName("quantity_after");
            entity.Property(e => e.QuantityChange)
                .HasPrecision(18, 3)
                .HasColumnName("quantity_change");

            entity.HasOne(d => d.Employee).WithMany(p => p.StockHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("stock_history_employee_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.StockHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_history_product_id_fkey");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("stores_pkey");

            entity.ToTable("stores");

            entity.Property(e => e.StoreId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("store_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.StoreAddress).HasColumnName("store_address");
            entity.Property(e => e.StoreCountryId).HasColumnName("store_country_id");
            entity.Property(e => e.StoreName).HasColumnName("store_name");
        });

        modelBuilder.Entity<StoresDocumentsType>(entity =>
        {
            entity.HasKey(e => e.StoreDocumentTypeId).HasName("stores_documents_types_pkey");

            entity.ToTable("stores_documents_types");

            entity.Property(e => e.StoreDocumentTypeId).HasColumnName("store_document_type_id");
            entity.Property(e => e.StoreDocumentTypeCategoryId).HasColumnName("store_document_type_category_id");
            entity.Property(e => e.StoreDocumentTypeDescription).HasColumnName("store_document_type_description");
            entity.Property(e => e.StoreDocumentTypeName).HasColumnName("store_document_type_name");
            entity.Property(e => e.StoreOrderTypeId).HasColumnName("store_order_type_id");
        });

        modelBuilder.Entity<StoresDocumentsTypesCategory>(entity =>
        {
            entity.HasKey(e => e.StoreDocumentTypeCategoryId).HasName("stores_documents_types_categories_pkey");

            entity.ToTable("stores_documents_types_categories");

            entity.Property(e => e.StoreDocumentTypeCategoryId).HasColumnName("store_document_type_category_id");
            entity.Property(e => e.CategoryName).HasColumnName("category_name");
        });

        modelBuilder.Entity<StoresOrdersType>(entity =>
        {
            entity.HasKey(e => e.StoreOrderTypeId).HasName("stores_orders_types_pkey");

            entity.ToTable("stores_orders_types");

            entity.Property(e => e.StoreOrderTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("store_order_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.StoreOrderTypeName).HasColumnName("store_order_type_name");
        });

        modelBuilder.Entity<SyncVersion>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("sync_versions_pkey");

            entity.ToTable("sync_versions", tb => tb.HasComment("Tabela przechowująca wersje synchronizacji zdarzeń magazynowych"));

            entity.HasIndex(e => e.SyncedAt, "idx_sync_versions_synced_at").IsDescending();

            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasColumnName("version");
            entity.Property(e => e.SyncedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("synced_at");
        });

        modelBuilder.Entity<UserContext>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_context_pkey");

            entity.ToTable("user_context");

            entity.HasIndex(e => e.UserId, "idx_user_context_user_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ContextData)
                .HasColumnType("jsonb")
                .HasColumnName("context_data");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Year>(entity =>
        {
            entity.HasKey(e => e.YearId).HasName("years_pkey");

            entity.ToTable("years");

            entity.HasIndex(e => e.YearNumber, "years_year_number_key").IsUnique();

            entity.Property(e => e.YearId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("year_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.YearNumber).HasColumnName("year_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
