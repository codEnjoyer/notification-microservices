from fastapi import APIRouter

from src.schemas.email import EmailMessageSchema
from src.schemas.tg import TgMessageSchema

router = APIRouter(
    prefix="/api",
)


# idempotency key?
@router.post(
    "/email",
)
async def root(
    payload: EmailMessageSchema,
) -> dict:
    return payload


@router.post(
    "/tg",
)
async def send_tg_message(
    payload: TgMessageSchema,
) -> dict:
    return payload
