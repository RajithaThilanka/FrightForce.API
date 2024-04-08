using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FrightForce.Infractructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentDomainDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ff_document_types",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ff_document_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ff_documents",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: true),
                    container_name = table.Column<string>(type: "text", nullable: false),
                    azure_blob_storage_url = table.Column<string>(type: "text", nullable: true),
                    document_type_id = table.Column<int>(type: "integer", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ff_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "privilege",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    resource = table.Column<string>(type: "text", nullable: false),
                    action = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_privilege", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "xb_dockets",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    container_name = table.Column<string>(type: "text", nullable: false),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    is_docket = table.Column<bool>(type: "boolean", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_xb_dockets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_role_privilege",
                schema: "public",
                columns: table => new
                {
                    privileges_id = table.Column<int>(type: "integer", nullable: false),
                    roles_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_role_privilege", x => new { x.privileges_id, x.roles_id });
                    table.ForeignKey(
                        name: "fk_application_role_privilege_privilege_privileges_id",
                        column: x => x.privileges_id,
                        principalSchema: "public",
                        principalTable: "privilege",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_application_role_privilege_roles_roles_id",
                        column: x => x.roles_id,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ff_docket_documents",
                schema: "public",
                columns: table => new
                {
                    docket_id = table.Column<int>(type: "integer", nullable: false),
                    document_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ff_docket_documents", x => new { x.docket_id, x.document_id });
                    table.ForeignKey(
                        name: "fk_ff_docket_documents_dockets_docket_id",
                        column: x => x.docket_id,
                        principalSchema: "public",
                        principalTable: "xb_dockets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ff_docket_documents_documents_document_id",
                        column: x => x.document_id,
                        principalSchema: "public",
                        principalTable: "ff_documents",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_application_role_privilege_roles_id",
                schema: "public",
                table: "application_role_privilege",
                column: "roles_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_roles_is_deleted",
                schema: "public",
                table: "AspNetRoles",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                schema: "public",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                schema: "public",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                schema: "public",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ff_docket_documents_document_id",
                schema: "public",
                table: "ff_docket_documents",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "ix_ff_document_types_code",
                schema: "public",
                table: "ff_document_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ff_documents_is_deleted",
                schema: "public",
                table: "ff_documents",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_xb_dockets_is_deleted",
                schema: "public",
                table: "xb_dockets",
                column: "is_deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_role_privilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ff_docket_documents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ff_document_types",
                schema: "public");

            migrationBuilder.DropTable(
                name: "privilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "xb_dockets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ff_documents",
                schema: "public");
        }
    }
}
