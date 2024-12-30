from typing import Annotated

from sqlalchemy.ext.asyncio import AsyncSession
from collections.abc import AsyncGenerator
from fastapi import Depends

from database import get_async_session
from src.services.esb_request import ESBRequestService


async def provide_esb_request_service(
    db_session: Annotated[AsyncSession, Depends(get_async_session)],
) -> AsyncGenerator[ESBRequestService, None]:
    async with ESBRequestService.new(session=db_session) as service:
        yield service


ESBRequestServiceDep = Annotated[
    ESBRequestService, Depends(provide_esb_request_service)
]
