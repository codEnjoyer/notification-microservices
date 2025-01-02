from contextlib import asynccontextmanager

from src.broker.broker import broker
from src.routes.api import router as api_router

from fastapi import FastAPI


@asynccontextmanager
async def lifespan(_: FastAPI):
    await broker.start()
    yield
    await broker.close()


app = FastAPI(lifespan=lifespan)
app.include_router(api_router)
