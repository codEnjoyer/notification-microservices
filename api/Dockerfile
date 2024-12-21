# An example using multi-stage image builds to create a final image without uv.

# First, build the application in the `/app` directory.
# See `Dockerfile` for details.
FROM ghcr.io/astral-sh/uv:python3.12-bookworm-slim AS builder

# Переопределяем стандартную команду запуска шелла для выполнения команд в форме "shell".
# https://docs.docker.com/reference/dockerfile/#shell-and-exec-form
# Опция `-e` включает мгновенный выход после ошибки для любой непроверенной команды.
#   Команда считается проверенной, если она используется в условии оператора ветвления (например, `if`)
#   или является левым операндом `&&` либо `||` оператора.
# Опция `-x` включает печать каждой команды в поток stderr перед её выполнением. Она очень полезна при отладке.
# https://manpages.ubuntu.com/manpages/noble/en/man1/sh.1.html
SHELL ["/bin/sh", "-exc"]


ENV UV_COMPILE_BYTECODE=1 UV_LINK_MODE=copy
WORKDIR /app
RUN --mount=type=cache,target=/root/.cache/uv \
    --mount=type=bind,source=uv.lock,target=uv.lock \
    --mount=type=bind,source=pyproject.toml,target=pyproject.toml \
    uv sync --frozen --no-install-project --no-dev
ADD . /app
RUN --mount=type=cache,target=/root/.cache/uv \
    uv sync --frozen --no-dev


# Then, use a final image without uv
FROM python:3.12-slim-bookworm
# It is important to use the image that matches the builder, as the path to the
# Python executable must be the same, e.g., using `python:3.11-slim-bookworm`
# will fail.

# Переопределяем стандартную команду запуска шелла для выполнения команд в форме "shell".
# https://docs.docker.com/reference/dockerfile/#shell-and-exec-form
# Опция `-e` включает мгновенный выход после ошибки для любой непроверенной команды.
#   Команда считается проверенной, если она используется в условии оператора ветвления (например, `if`)
#   или является левым операндом `&&` либо `||` оператора.
# Опция `-x` включает печать каждой команды в поток stderr перед её выполнением. Она очень полезна при отладке.
# https://manpages.ubuntu.com/manpages/noble/en/man1/sh.1.html
SHELL ["/bin/sh", "-exc"]


# Copy the application from the builder
COPY --from=builder --chown=app:app /app /app

# Place executables in the environment at the front of the path
ENV PATH="/app/.venv/bin:$PATH"

# Переходим в папку app, где уже находится наш проект
WORKDIR /app

ENTRYPOINT ["./docker-entrypoint.sh"]
