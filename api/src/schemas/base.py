from typing import Annotated

from pydantic import (
    BaseModel,
    ConfigDict,
    Field,
    PositiveInt,
)

IntID = Annotated[
    PositiveInt,
    Field(description="Численный идентификатор", examples=[1, 768519797]),
]
NonEmptyString = Annotated[
    str,
    Field(min_length=1, description="Не пустая строка", examples=["string", "s"]),
]
type DictStrAny = dict[str, ...]


class BaseSchema(BaseModel):
    model_config = ConfigDict(from_attributes=True)
