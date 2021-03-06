FROM khudikea/ea_contrib:1.0.0

USER root

COPY ./entrypoint.sh /home/tizen/
RUN chown tizen /home/tizen/entrypoint.sh && \
    chmod 777 /home/tizen/entrypoint.sh

USER tizen 

COPY --chown=tizen ./dist/*.* /home/tizen/projects/testProject/

ENTRYPOINT /home/tizen/entrypoint.sh && /bin/sh