from pydantic import Field, PostgresDsn, SecretStr
from pydantic_settings import BaseSettings, SettingsConfigDict


class DatabaseSettings(BaseSettings):
    model_config = SettingsConfigDict(extra="allow", env_file="../.env")

    host: str = Field(validation_alias="DB_HOST")
    port: int = Field(validation_alias="DB_PORT")
    password: SecretStr = Field(validation_alias="DB_PASSWORD")
    name: str = Field(validation_alias="DB_NAME")
    user: SecretStr = Field(validation_alias="DB_USER")

    @property
    def url(self) -> str:
        host = PostgresDsn.build(
            scheme="postgresql+asyncpg",
            username=self.user.get_secret_value(),
            password=self.password.get_secret_value(),
            host=self.host,
            port=self.port,
            path=self.name,
        )
        return str(host)


db_settings = DatabaseSettings()
