import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoaderService } from "../shared/loader.service";
import { tap } from 'rxjs/operators';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private loaderService: LoaderService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log("interceptor works!!")

    this.loaderService.requestStarted();
    return this.handler(next, req);
  }


  handler(next, req) {
    return next.handle(req)
      .pipe(
        tap(
          data => {
            if (data instanceof HttpResponse) {
              this.loaderService.requestEnded();
            }
          },
          (error: HttpErrorResponse) => {
            this.loaderService.resetLoader();
            throw error;
          }
        ),
      );
  }
}
